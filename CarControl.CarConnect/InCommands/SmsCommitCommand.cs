using CarControl.Service;

namespace CarControl.CarConnect.InCommands
{
    public class SmsCommitCommand : IInCommand
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
