using System.Collections.Generic;
using System.Net;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Protocol;
using CarControl.Service;

namespace CarControl.CarConnect.Server
{
    public class CarProtoServer: Disposable, ICarProtoServer
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ICarService _carService;
        private readonly TcpServer _tcpServer;
        private int _nextId = 1;
        private readonly List<ICarProtocol> _cars = new List<ICarProtocol>();

        public CarProtoServer(ICarService carService, ICommandFactory commandFactory)
        {
            _carService = carService;
            _commandFactory = commandFactory;
            _tcpServer = new TcpServer(255);
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 4999);
            _tcpServer.Start(ipEndPoint);
            _tcpServer.ClientConnected += ClientConnected;
            _tcpServer.ClientDisonnected += ClientDisconnected;
        }

        private void ClientConnected(object sender, AsyncCarClientToken e)
        {
            var cp = new TextAuthProto(e, _commandFactory, _nextId++, -1, _carService);
            e.Proto = cp;
            _cars.Add(cp);
        }

        private void ClientDisconnected(object sender, AsyncCarClientToken e)
        {
            _cars.Remove(e.Proto);
        }

        public IEnumerable<ICarProtocol> GetConnections()
        {
            return _cars;
        }

        public ICarProtocol GetConnection(int id)
        {
            return _cars.Find(p => p.Id == id);
        }

        protected override void DisposeCore()
        {
            _tcpServer?.Dispose();
        }
    }
}
