using CarControl.CarConnect.Protocol;

namespace CarControl.CarConnect.Server
{
    public interface ITcpConnection
    {
        void Send(byte[] bufBytes);
        void Close();
        void SetProtocol(ICarProtocol proto);
    }
}
