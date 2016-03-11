using System.Net.Sockets;
using CarControl.CarConnect.Protocol;
using CarControl.CarConnect.Server;

namespace CarConnect.Test
{
    public class TcpConnectionMock : ITcpConnection
    {
        public byte[] LastSentBuf { get; set; }
        public bool Closed { get; set; }

        public void Close()
        {
            Closed = true;
        }

        public Socket Socket { get; }

        public void Send(byte[] bufBytes)
        {
            LastSentBuf = bufBytes;
        }

        public void SetProtocol(ICarProtocol proto)
        {

        }

        public TcpConnectionMock()
        {
            Socket = null;
        }
    }
}
