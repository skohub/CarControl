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
        void Ping(int connectionId);

        [OperationContract(IsOneWay = true)]
        void Start(int connectionId);

        [OperationContract]
        List<int> ConnectionList();

        [OperationContract]
        List<CarDto> ConnectedCars();

        [OperationContract]
        CarDto GetCar(int carId);
    }

    public interface ICarCommandCallback
    {
        [OperationContract(IsOneWay = true)]
        void Notify(string property, string value);
    }
}
