using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarControl.Contract;

namespace CarControl.CarConnect.Commands
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
