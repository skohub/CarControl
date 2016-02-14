using System;
using CarControl.CarConnect.CommandsCommon;
using CarControl.CarConnect.InputCommands;
using CarControl.CarConnect.Server;
using CarControl.Service;

namespace CarControl.CarConnect.Protocol
{
    public class TextAuthProto : BaseTextProto
    {
        private readonly ICarService _carService;

        public TextAuthProto(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId,
            ICarService carService1) : base(connection, commandFactory, id, carId)
        {
            _carService = carService1;
        }

        public override void CommandReceived(string cmd)
        {
            var parts = cmd.Split(':');
            if (parts.Length < 2)
            {
                Send("Parol ne nayden");
                Connection.Close(); 
                return;
            }

            var imei = parts[0];
            var hash = parts[1];
            var car = _carService.GetCarByImei(imei);
            var loginok = (car != null) && (hash == car.Hash);
            if (loginok)
            {
                CarTimeGround = DateTime.Now;
                var minuteOfYear = (int)(CarTimeGround.Subtract(new DateTime(DateTime.Now.Year, 1, 1))).TotalSeconds / 60;
                var ans = "LOGINOK\r\n" +
                          "SETTIME: " + minuteOfYear.ToString().PadLeft(6, '0');
                Send(ans);
                CarId = car.CarId;
                SetProtocol(new TextProto());
            }
            else
            {
                Send("LOGINFAIL");
            }
        }
    }
}
