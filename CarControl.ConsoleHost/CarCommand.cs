using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using CarConnect.Model;
using CarControl.CarConnect.Protocol;
using CarControl.CarConnect.Server;
using CarControl.Contract;
using CarControl.Service;

namespace CarControl.ConsoleHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CarCommand : ICarCommand
    {
        private readonly IMapper _mapper;
        private readonly ICarProtoServer _carProtoServer;
        private readonly ICarService _carService;

        public CarCommand(IMapper mapper, ICarProtoServer carProtoServer, ICarService carService)
        {
            _mapper = mapper;
            _carProtoServer = carProtoServer;
            _carService = carService;
        }

        public void Ping(int carId)
        {
            _carProtoServer.GetConnection(carId).Send("PING");
        }

        public List<int> ConnectionList()
        {
            return _carProtoServer.GetConnections().Select(carProtocol => carProtocol.Id).ToList();
        }

        public CarDto GetCar(int connectionId)
        {
            var carId = _carProtoServer.GetConnection(connectionId).CarId;
            var car = _carService.GetCar(carId);
            return _mapper.Map<Car, CarDto>(car);
        }

        ICarCommandCallback Callback => OperationContext.Current.GetCallbackChannel<ICarCommandCallback>();
    }
}
