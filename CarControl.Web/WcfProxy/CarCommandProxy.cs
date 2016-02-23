using System.Collections.Generic;
using System.ServiceModel;
using CarControl.Contract;

namespace CarControl.Web.WcfProxy
{
    public class CarCommandProxy : ClientBase<ICarCommand>, ICarCommand
    {
        public CarCommandProxy(InstanceContext callbackInstance) : base(callbackInstance) { }

        public void Ping(int connectionId)
        {
            Channel.Ping(connectionId);
        }

        public void Start(int connectionId)
        {
            Channel.Start(connectionId);
        }

        public List<int> ConnectionList()
        {
            return Channel.ConnectionList();
        }

        public List<CarDto> ConnectedCars()
        {
            return Channel.ConnectedCars();
        }

        public CarDto GetCar(int carId)
        {
            return Channel.GetCar(carId);
        }
    }
}