using CarControl.CarConnect.Protocol;

namespace CarControl.CarConnect.Commands
{
    public class CommandModeCommand : ICommand
    {
        private readonly ICarProtocol _carProtocol;
        private readonly string _parameter;

        public CommandModeCommand(ICarProtocol carProtocol, string parameter)
        {
            _carProtocol = carProtocol;
            _parameter = parameter;
        }

        public bool Execute()
        {
            switch (_parameter)
            {
                case "BINARY":
                    _carProtocol.Send("SET BINARY PROTOCOL");
                    _carProtocol.SetProtocol(new BinaryProto());
                    return true;
                case "TEXT":
                    _carProtocol.Send("SET TEXT PROTOCOL");
                    _carProtocol.SetProtocol(new TextProto());
                    return true;
                default:
                    _carProtocol.Send("PROTOCOL NOT FOUND");
                    return false;
            }
        }
    }
}
