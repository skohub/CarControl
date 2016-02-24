using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CarControl.CarConnect.CommandsCommon;
using CarControl.CarConnect.Server;
using CarControl.Service;

namespace CarControl.CarConnect.Protocol
{
    public class CarProtoServer: Disposable, ICarProtoServer
    {
        private readonly ICarService _carService;
        private readonly TcpServer _tcpServer;
        private int _nextId = 1;
        private readonly Dictionary<int, ICarProtocol> _cars = new Dictionary<int, ICarProtocol>();
        public Func<ICommandFactory> GetInputCommandFactory { get; set; }
        public Action OnClientDisconnected { get; set; }

        public CarProtoServer(ICarService carService)
        {
            _carService = carService;
            _tcpServer = new TcpServer(255);
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 4999);
            _tcpServer.Start(ipEndPoint);
            _tcpServer.ClientConnected += ClientConnected;
            _tcpServer.ClientDisonnected += ClientDisconnected;
        }

        private void ClientConnected(object sender, AsyncCarClientToken e)
        {
            if (GetInputCommandFactory == null) return;
            var commandFactory = GetInputCommandFactory();
            var cp = new TextAuthProto(e, commandFactory, _nextId++, _carService);
            e.Proto = cp;
            _cars.Add(cp.Id, cp);
        }

        private void ClientDisconnected(object sender, AsyncCarClientToken e)
        {
            _cars.Remove(e.Proto.Id);
            OnClientDisconnected?.Invoke();
        }

        public IEnumerable<ICarProtocol> GetConnections()
        {
            return _cars.Values;
        }

        public ICarProtocol GetConnection(int id)
        {
            return _cars.Values.ToList().Find(p => p.Id == id);
        }

        protected override void DisposeCore()
        {
            _tcpServer?.Dispose();
        }
    }
}
