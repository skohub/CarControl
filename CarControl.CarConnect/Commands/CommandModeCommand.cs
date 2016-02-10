using CarControl.CarConnect.Protocol;

namespace CarControl.CarConnect.Commands
{
    public class CommandModeCommand : IInputCommand
    {
        private readonly ICarProtocol _carProtocol;
        private readonly string _parameter;

        public CommandModeCommand(ICarProtocol carProtocol, string parameter)
        {
            _carProtocol = carProtocol;
            _parameter = parameter;
        }

        public void Execute()
        {
            switch (_parameter)
            {
                case "BINARY":
                    _carProtocol.Send("SET BINARY PROTOCOL");
                    _carProtocol.SetProtocol(new BinaryProto());
                    break;
                case "TEXT":
                    _carProtocol.Send("SET TEXT PROTOCOL");
                    _carProtocol.SetProtocol(new TextProto());
                    break;
                default:
                    _carProtocol.Send("PROTOCOL NOT FOUND");
                    break;
            }
        }
    }
}
