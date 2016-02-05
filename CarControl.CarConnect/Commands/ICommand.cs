using CarControl.CarConnect.Protocol;

namespace CarControl.CarConnect.Commands
{
    public interface ICommand
    {
        bool Execute();
    }
}
