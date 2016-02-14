using System;
using System.Collections.Generic;
using CarControl.CarConnect.CommandsCommon;

namespace CarControl.CarConnect.Protocol
{
    public interface ICarProtoServer
    {
        IEnumerable<ICarProtocol> GetConnections();
        ICarProtocol GetConnection(int id);
        Func<ICommandFactory> GetInputCommandFactory { get; set; }
        Action OnClientDisconnected { get; set; }
    }
}