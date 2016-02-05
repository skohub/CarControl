using CarConnect.Data.Infrastructure;
using CarConnect.Data.Repositories;
using CarConnect.Model;
using CarControl.Contract;

namespace CarControl.Service
{
    public class SmsService : ISmsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISmsRepository _smsRepository;

        public SmsService(IUnitOfWork unitOfWork, ISmsRepository smsRepository)
        {
            _unitOfWork = unitOfWork;
            _smsRepository = smsRepository;
        }

        public void CreateSms(Sms sms)
        {
            _smsRepository.Add(sms);
            
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
