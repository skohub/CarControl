using System.Net.Sockets;
using CarControl.CarConnect.Protocol;

namespace CarControl.CarConnect.Server
{
    public interface ITcpConnection
    {
        Socket Socket { get; }
        void Send(byte[] bufBytes);
        void Close();
        void SetProtocol(ICarProtocol proto);
    }
}
