using CarConnect.Data.Infrastructure;
using CarConnect.Data.Repositories;
using CarConnect.Model;

namespace CarControl.Service
{
    public interface ISmsService
    {
        void CreateSms(Sms sms);
        void Commit();
    }

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
