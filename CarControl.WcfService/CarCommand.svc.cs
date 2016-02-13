using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using CarControl.CarConnect.Server;
using CarControl.Contract;

namespace CarControl.WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CarCommand : ICarCommand
    {
        private readonly ICarProtoServer _carProtoServer;

        public CarCommand(ICarProtoServer carProtoServer)
        {
            _carProtoServer = carProtoServer;
        }

        public void Ping(int carId)
        {
            _carProtoServer.GetConnection(carId).Send("PING");
        }

        public List<int> ConnectionList()
        {
            return _carProtoServer.GetConnections().Select(carProtocol => carProtocol.Id).ToList();
        }

        private ICarCommandCallback Callback => OperationContext.Current.GetCallbackChannel<ICarCommandCallback>();
    }
}
