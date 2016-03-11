using System;
using System.Net.Sockets;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.CarConnect.InputCommands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    public interface ICarProtocol
    {
        int Id { get; }
        Car Car { get; }
        ITcpConnection Connection { get; }
        void Receive(byte[] bufBytes);
        void Send(byte[] bufBytes);
        void Send(string cmd);
        void Init(ITcpConnection connection, ICommandFactory commandFactory, int id, Car car);
        void SetProtocol(ICarProtocol protocol);
    }
}
