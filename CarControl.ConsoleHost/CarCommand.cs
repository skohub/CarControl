using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CarControl.CarConnect.Server;
using CarControl.Contract;

namespace CarControl.ConsoleHost
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

        ICarCommandCallback Callback => OperationContext.Current.GetCallbackChannel<ICarCommandCallback>();
    }
}
