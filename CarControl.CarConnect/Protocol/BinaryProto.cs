using System;
using System.Diagnostics;
using System.Text;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Server;

namespace CarControl.CarConnect.Protocol
{
    class BinaryProto : BaseBinaryProtocol
    {
        public BinaryProto(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId)
            : base(connection, commandFactory, id, carId)
        {
        }

        public BinaryProto() { }

        public override void CommandReceived(byte[] bufBytes)
        {
            Assert(bufBytes.Length > 1, "Received data length must be > 1");
            ICommand command = null;
            switch (bufBytes[0])
            {
                case CommandMode:
                    var commandMode = bufBytes[1] == 0 ? "TEXT" : "BINARY";
                    command = CommandFactory.CreateCommandMode(this, commandMode);
                    break;
                case Temp1:
                    var paramLength = bufBytes[1];
                    var parameterBytes = new byte[paramLength];
                    Array.Copy(bufBytes, 2, parameterBytes, 0, paramLength);
                    var parameter = Encoding.UTF8.GetString(parameterBytes);
                    int temperature;
                    Assert(int.TryParse(parameter, out temperature), "BAD TEMPERATURE");
                    var minutes = BitConverter.ToInt32(bufBytes, 2 + paramLength);
                    var time = CarTimeGround.AddMinutes(minutes);
                    command = CommandFactory.CreateTemp1(CarId, temperature, time);
                    break;
            }
        }

    }
}
