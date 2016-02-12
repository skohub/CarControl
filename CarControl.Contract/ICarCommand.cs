using System.Collections.Generic;
using System.ServiceModel;

namespace CarControl.Contract
{
    [ServiceContract(
        Namespace = "http://Microsoft.ServiceModel.Samples",
        SessionMode = SessionMode.Required,
        CallbackContract = typeof(ICarCommandCallback)
    )]
    public interface ICarCommand
    {
        [OperationContract(IsOneWay = true)]
        void Ping(int carId);

        [OperationContract]
        List<int> ConnectionList();
    }

    public interface ICarCommandCallback
    {
        [OperationContract(IsOneWay = true)]
        void Notify(string property, string value);
    }
}
