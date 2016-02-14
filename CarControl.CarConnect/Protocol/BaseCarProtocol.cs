using System;
using CarControl.CarConnect.InCommands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    public class BaseCarProtocol: ICarProtocol
    {
        public static string ProtoError = "Protocol error";
        public static string WrongLength = "Data length must be at least {0} bytes";
        public static string Unauthorized = "Unauthorized";
        public static string Authorized = "Login ok";
        public static string WrongAction = "Action unknown";

        private bool _initilized;

        protected int CarId;
        protected ICommandFactory CommandFactory;
        protected ITcpConnection Connection;
        protected DateTime CarTimeGround; // минуты машины передаются относительно этого времени
        
        public int Id { get; protected set; }

        public BaseCarProtocol(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId)
        {
            Init(connection, commandFactory, id, DateTime.MinValue, carId);
        }

        public BaseCarProtocol() { }

        public virtual void Receive(byte[] bufBytes)
        {
            throw new NotImplementedException();
        }

        public virtual void Send(byte[] bufBytes)
        {
            Connection.Send(bufBytes);
        }

        public virtual void Send(string cmd)
        {
            throw new NotImplementedException();
        }

        public void Init(ITcpConnection connection, ICommandFactory commandFactory, int id, DateTime carTimeGround, int carId)
        {
            Connection = connection;
            Id = id;
            CarId = carId;
            CommandFactory = commandFactory;
            CarTimeGround = carTimeGround;
            _initilized = true;
        }

        public void SetProtocol(ICarProtocol protocol)
        {
            if (!_initilized) throw new InvalidOperationException();
            protocol.Init(Connection, CommandFactory, Id, CarTimeGround, CarId);
            Connection.SetProtocol(protocol);
        }

        protected void Assert(bool condition, string message, bool closeConnection = false)
        {
            if (condition) return;
            Send("COMMANDFAIL");
            Send(message);
            if (closeConnection) Connection.Close();
            throw new InvalidOperationException();
        }
    }
}
