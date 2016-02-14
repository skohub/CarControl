using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class SmsCommitCommand : IInputCommand
    {
        private readonly ISmsService _smsService;

        public SmsCommitCommand(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public void Execute()
        {
            _smsService.Commit();
        }
    }
}
