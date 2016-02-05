// Implements the connection logic for the socket server.  
// After accepting a connection, all data read from the client 
// is sent back to the client. The read and echo back to the client pattern 
// is continued until the client disconnects.

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CarControl.CarConnect.Server
{
    public class TcpServer: Disposable
    {
        public event EventHandler<AsyncCarClientToken> ClientConnected;
        public event EventHandler<AsyncCarClientToken> ClientDisonnected;

        public readonly int ReceiveBufferSize; // buffer size to use for each socket I/O operation 
        Socket _listenSocket; // the socket used to listen for incoming connection requests
        // pool of reusable SocketAsyncEventArgs objects for write, read and accept socket operations
        int _totalBytesRead; // counter of the total # bytes received by the server
        int _numConnectedSockets; // the total number of clients connected to the server 

        // Create an uninitialized server instance.  
        // To start the server listening for connection requests
        // call the Init method followed by Start method 
        //
        // <param name="numConnections">the maximum number of connections the sample is designed to handle simultaneously</param>
        // <param name="receiveBufferSize">buffer size to use for each socket I/O operation</param>
        public TcpServer(int receiveBufferSize)
        {
            _totalBytesRead = 0;
            _numConnectedSockets = 0;
            ReceiveBufferSize = receiveBufferSize;
        }


        // Starts the server such that it is listening for 
        // incoming connection requests.    
        //
        // <param name="localEndPoint">The endpoint which the server will listening 
        // for connection requests on</param>
        public void Start(IPEndPoint localEndPoint)
        {
            // create the socket which listens for incoming connections
            _listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listenSocket.Bind(localEndPoint);
            // start the server with a listen backlog of 100 connections
            _listenSocket.Listen(4999);

            // post accepts on the listening socket
            StartAccept(null);
        }


        // Begins an operation to accept a connection request from the client 
        //
        // <param name="acceptEventArg">The context object to use when issuing 
        // the accept operation on the server's listening socket</param>
        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += AcceptEventArg_Completed;
            }
            else
            {
                // socket must be cleared since the context object is being reused
                acceptEventArg.AcceptSocket = null;
            }

            bool willRaiseEvent = _listenSocket.AcceptAsync(acceptEventArg);
            if (!willRaiseEvent)
            {
                ProcessAccept(acceptEventArg);
            }
        }

        // This method is the callback method associated with Socket.AcceptAsync 
        // operations and is invoked when an accept operation is complete
        //
        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Interlocked.Increment(ref _numConnectedSockets);
            
            // Get the socket for the accepted client connection and put it into the 
            //ReadEventArg object user token
            var readEventArgs = new SocketAsyncEventArgs();
            var buf = new byte[ReceiveBufferSize];
            readEventArgs.Completed += IO_Completed;
            readEventArgs.SetBuffer(buf, 0, ReceiveBufferSize);
            var token = new AsyncCarClientToken(readEventArgs, this);
            readEventArgs.UserToken = token;
            token.Socket = e.AcceptSocket;

            OnClientConnected(token);

            // As soon as the client is connected, post a receive to the connection
            bool willRaiseEvent = e.AcceptSocket.ReceiveAsync(readEventArgs);
            if (!willRaiseEvent)
            {
                ProcessReceive(readEventArgs);
            }

            // Accept the next connection request
            StartAccept(e);
        }

        // This method is called whenever a receive or send operation is completed on a socket 
        //
        // <param name="e">SocketAsyncEventArg associated with the completed receive operation</param>
        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            var token = (AsyncCarClientToken)e.UserToken;
            if (token.Closed) return;
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }

        }

        // This method is invoked when an asynchronous receive operation completes. 
        // If the remote host closed the connection, then the socket is closed.  
        // If data was received then the data is echoed back to the client.
        //
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            // check if the remote host closed the connection
            var token = (AsyncCarClientToken)e.UserToken;
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //increment the count of the total bytes receive by the server
                Interlocked.Add(ref _totalBytesRead, e.BytesTransferred);
                var bufBytes = new byte[e.BytesTransferred];
                Buffer.BlockCopy(e.Buffer, e.Offset, bufBytes, 0, e.BytesTransferred);
                token.DataReceived(bufBytes);

                // read the next block of data send from the client
                if (e.LastOperation != SocketAsyncOperation.Receive) return;
                if (token.Closed) return;
                var willRaiseEvent = token.Socket.ReceiveAsync(e);
                if (!willRaiseEvent) ProcessReceive(e);
                //echo the data received back to the client
                /*
                e.SetBuffer(e.Offset, e.BytesTransferred);

                bool willRaiseEvent = token.Socket.SendAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessSend(e);
                }
                */
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        // This method is invoked when an asynchronous send operation completes.  
        // The method issues another receive on the socket to read any additional 
        // data sent from the client
        //
        // <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // done echoing data back to the client
                var token = (AsyncCarClientToken)e.UserToken;
                // read the next block of data send from the client
                var willRaiseEvent = token.Socket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        public void CloseClientSocket(SocketAsyncEventArgs e)
        {
            var token = e.UserToken as AsyncCarClientToken;
            OnClientDisonnected(token);

            // close the socket associated with the client
            if (token?.Socket != null)
            {
                token.Socket.Shutdown(SocketShutdown.Both);
                token.Socket.Close();
            }

            // decrement the counter keeping track of the total number of clients connected to the server
            Interlocked.Decrement(ref _numConnectedSockets);
        }


        protected override void DisposeCore()
        {
            _listenSocket.Close();
            _listenSocket.Dispose();
        }

        protected virtual void OnClientConnected(AsyncCarClientToken e)
        {
            ClientConnected?.Invoke(this, e);
        }

        protected virtual void OnClientDisonnected(AsyncCarClientToken e)
        {
            ClientDisonnected?.Invoke(this, e);
        }
    }
}