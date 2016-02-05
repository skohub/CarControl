using System;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    public interface ICarProtocol
    {
        int Id { get; }
        void Receive(byte[] bufBytes);
        void Send(byte[] bufBytes);
        void Send(string cmd);
        void Init(ITcpConnection connection, ICommandFactory commandFactory, int id, DateTime carTimeGround, int carId);
        void SetProtocol(ICarProtocol protocol);
    }
}
