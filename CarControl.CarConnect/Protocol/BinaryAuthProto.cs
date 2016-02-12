using System;
using System.Text;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Server;
using CarControl.Service;

namespace CarControl.CarConnect.Protocol
{
    public class BinaryAuthProto : BaseBinaryProtocol
    {
        private readonly ICarService _carService;
        private State _state = State.Unauthorized;

        public enum State { Unauthorized, Authorized };
        public enum Action : byte { Auth = 1, Register = 2 }

        public BinaryAuthProto(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId,
            ICarService carService) : base(connection, commandFactory, id, carId)
        {
            _carService = carService;
        }

        public override void CommandReceived(byte[] bufBytes)
        {
            switch (_state)
            {
                case State.Unauthorized:
                    if (bufBytes.Length < 1)
                    {
                        Send(Encoding.UTF8.GetBytes(string.Format(WrongLength, 1)));
                        Connection.Close();
                        return;
                    }
                    var action = bufBytes[0];
                    switch (action)
                    {
                        case (byte)Action.Auth:
                            if (Authorize(bufBytes))
                            {
                                Send(1, 1, Authorized);
                                _state = State.Authorized;
                                //Connection.SetProtocol(new TextProto(Connection, Id));
                            }
                            else
                            {
                                var msg = string.Format(WrongLength, 2);
                                Send(1 /* login */, 0 /* failed */, msg);
                                Connection.Close();
                            }
                            break;
                        default:
                            Send(1, 0, WrongAction);
                            //Connection.Close();
                            break;
                    }
                    break;
            }
        }

        public void Send(byte action, byte result, string message)
        {
            var msgBytes = Encoding.UTF8.GetBytes(message);
            var msgLength = msgBytes.Length < 250 ? msgBytes.Length : 250;
            var answer = new byte[3 + msgLength];
            answer[0] = action;
            answer[1] = result;
            answer[2] = (byte) msgLength;
            Array.Copy(msgBytes, 0, answer, 3, msgLength);
            base.Send(answer);
        }

        private bool Authorize(byte[] bufBytes)
        {
            if (bufBytes.Length < 3) return false;
            var imeiLength = bufBytes[1];
            var passLength = bufBytes[2];
            if ((imeiLength < 1) || (imeiLength > 125)) return false;
            if ((passLength < 1) || (passLength > 125)) return false;
            if (bufBytes.Length < 3 + imeiLength + passLength) return false;
                    
            var imeiBytes = new byte[imeiLength];
            var passBytes = new byte[passLength];
            Array.Copy(bufBytes, 3, imeiBytes, 0, imeiLength);
            Array.Copy(bufBytes, 3 + imeiLength, passBytes, 0, passLength);
            var imei = Encoding.UTF8.GetString(imeiBytes);
            var hash = Encoding.UTF8.GetString(passBytes);
            var car = _carService.GetCarByImei(imei);
            return hash == car.Hash;
        }

    }
}
