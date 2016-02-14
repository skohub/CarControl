using System;
using System.Collections.Generic;
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
        private readonly List<ICarProtocol> _cars = new List<ICarProtocol>();
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
            var cp = new TextAuthProto(e, commandFactory, _nextId++, -1, _carService);
            e.Proto = cp;
            _cars.Add(cp);
        }

        private void ClientDisconnected(object sender, AsyncCarClientToken e)
        {
            _cars.Remove(e.Proto);
            OnClientDisconnected?.Invoke();
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
