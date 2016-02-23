using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using CarConnect.Model;
using CarControl.CarConnect.Protocol;
using CarControl.Contract;

namespace CarControl.ConsoleHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CarCommand : ICarCommand
    {
        private readonly IMapper _mapper;
        private readonly ICarProtoServer _carProtoServer;

        public CarCommand(IMapper mapper, ICarProtoServer carProtoServer)
        {
            _mapper = mapper;
            _carProtoServer = carProtoServer;
        }

        public void Ping(int connectionId)
        {
            _carProtoServer.GetConnection(connectionId).Send("PING");
        }

        public void Start(int connectionId)
        {
            _carProtoServer.GetConnection(connectionId).Send("START");
        }

        public List<int> ConnectionList()
        {
            return _carProtoServer.GetConnections().Select(carProtocol => carProtocol.Id).ToList();
        }

        public List<CarDto> ConnectedCars()
        {
            var cars = new List<CarDto>();
            foreach (var carProtocol in _carProtoServer.GetConnections())
            {
                var car = _mapper.Map<Car, CarDto>(carProtocol.Car);
                if (car == null) continue;
                car.ConnectionId = carProtocol.Id;
                cars.Add(car);
            }
            return cars;
        }

        public CarDto GetCar(int connectionId)
        {
            var car = _carProtoServer.GetConnection(connectionId).Car;
            return _mapper.Map<Car, CarDto>(car);
        }

        ICarCommandCallback Callback => OperationContext.Current.GetCallbackChannel<ICarCommandCallback>();
    }
}
