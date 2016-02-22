using System;
using CarControl.CarConnect.CommandsCommon;
using CarControl.CarConnect.InputCommands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    public interface ICarProtocol
    {
        int Id { get; }
        int CarId { get; }
        void Receive(byte[] bufBytes);
        void Send(byte[] bufBytes);
        void Send(string cmd);
        void Init(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId);
        void SetProtocol(ICarProtocol protocol);
    }
}
