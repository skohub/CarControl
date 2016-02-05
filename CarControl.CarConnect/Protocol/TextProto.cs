using CarControl.CarConnect.Server;
using System;
using System.Globalization;
using CarControl.CarConnect.Commands;
using NLog;

namespace CarControl.CarConnect.Protocol
{
    public class TextProto : BaseTextProto
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public TextProto(ITcpConnection connection, ICommandFactory commandFactory, int id, int carId)
            : base(connection, commandFactory, id, carId)
        {
        }

        public TextProto()
        {
        }

        private DateTime ParseMinutes(string value)
        {
            int minutes;
            Assert(int.TryParse(value, out minutes), "BAD MINUTES");
            return new DateTime(DateTime.Now.Year, 1, 1).AddMinutes(minutes);
        }

        public override void CommandReceived(string text)
        {
            _logger.Debug(text);
            if (text.Length < 1) return;
            var cmd = text.Split(':');
            var action = cmd[0];
            ICommand command = null;
            switch (action)
            {
                case "COMMANDMODE":
                    Assert(cmd.Length == 2, "COMMANDMODE awaits 1 param");
                    command = CommandFactory.CreateCommandMode(this, cmd[1]);
                    break;
                case "T1":
                    Assert(cmd.Length == 3, "T1 awaits 2 param");
                    int temperatureInt;
                    Assert(int.TryParse(cmd[1], out temperatureInt), "TEMPERATURE BAD");
                    // чтобы не париться с запятой передаем целым
                    var temperature = temperatureInt/10f;
                    command = CommandFactory.CreateTemp1(CarId, temperature, ParseMinutes(cmd[2]));
                    break;
                case "V":
                    Assert(cmd.Length == 3, "V awaits 2 param");
                    int voltageInt;
                    Assert(int.TryParse(cmd[1], out voltageInt), "TEMPERATURE BAD");
                    // чтобы не париться с запятой передаем целым
                    var voltage = voltageInt / 10f;
                    command = CommandFactory.CreateVoltage(CarId, voltage, ParseMinutes(cmd[2]));
                    break;
                case "G":
                    Assert(cmd.Length == 5, "G awaits 4 param");
                    command = CommandFactory.CreateGSensor(CarId, int.Parse(cmd[1]), int.Parse(cmd[2]), int.Parse(cmd[3]),
                        ParseMinutes(cmd[4]));
                    break;
                case "GPS":
                    Assert(cmd.Length == 4, "GPS awaits 3 param");
                    command = CommandFactory.CreateGps(CarId, double.Parse(cmd[1], CultureInfo.InvariantCulture),
                        double.Parse(cmd[2], CultureInfo.InvariantCulture), ParseMinutes(cmd[3]));
                    break;
                case "SMSIN":
                    Assert(cmd.Length == 3, "SMSIN awaits 2 param");
                    command = CommandFactory.CreateSmsIn(CarId, cmd[1], ParseMinutes(cmd[2]));
                    break;
                case "SMSOUT":
                    Assert(cmd.Length == 3, "SMSOUT awaits 2 param");
                    command = CommandFactory.CreateSmsOut(CarId, cmd[1], ParseMinutes(cmd[2]));
                    break;
                case "SMSC":
                    command = CommandFactory.CreateSmsCommit();
                    break;
                case "SP":
                    Assert(cmd.Length == 3, "SP awaits 2 param");
                    command = CommandFactory.CreateSpeed(CarId, int.Parse(cmd[1]), ParseMinutes(cmd[2]));
                    break;
            }
            if (command != null)
            {
                Send(command.Execute() ? "COMMANDOK" : "COMMANDFAIL");
            }
            else
            {
                Send("COMMAND NOT FOUND");
            }
                
        }
    }
}
