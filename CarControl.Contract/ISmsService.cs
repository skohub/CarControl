using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;

namespace CarControl.Contract
{
    [ServiceContract]
    public interface ISmsService
    {
        [OperationContract]
        void CreateSms(Sms sms);

        void Commit();
    }
}
