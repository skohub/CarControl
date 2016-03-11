using System;
using System.Net.Sockets;
using CarControl.CarConnect.Protocol;

namespace CarControl.CarConnect.Server
{
    public class AsyncCarClientToken: ITcpConnection
    {
        private readonly SocketAsyncEventArgs _socketAsyncEventArgs;
        private readonly TcpServer _tcpServer;
        public bool Closed { get; set; }

        public Socket Socket { get; set; }
        public ICarProtocol Proto { get; set; }

        public AsyncCarClientToken(SocketAsyncEventArgs socketAsyncEventArgs, TcpServer tcpServer)
        {
            _socketAsyncEventArgs = socketAsyncEventArgs;
            _tcpServer = tcpServer;
        }

        public void DataReceived(byte[] bufBytes)
        {
            if (Closed) return;
            Proto?.Receive(bufBytes);
        }

        public void Send(byte[] bufBytes)
        {
            if (Closed) return;
            Array.Clear(_socketAsyncEventArgs.Buffer, 0, _socketAsyncEventArgs.Count);
            //var dataLength = (bufBytes.Length > _tcpServer.ReceiveBufferSize ? _tcpServer.ReceiveBufferSize : bufBytes.Length);
            //Buffer.BlockCopy(bufBytes, 0, _socketAsyncEventArgs.Buffer, 0, dataLength);
            Socket.Send(bufBytes);
            //Socket.SendAsync(_socketAsyncEventArgs);
        }

        public void Close()
        {
            Closed = true;
            _tcpServer.CloseClientSocket(_socketAsyncEventArgs);
        }

        public void SetProtocol(ICarProtocol proto)
        {
            Proto = proto;
        }

    }
}
