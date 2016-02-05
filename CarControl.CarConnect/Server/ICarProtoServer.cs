using System.Collections.Generic;
using CarControl.CarConnect.Protocol;

namespace CarControl.CarConnect.Server
{
    public interface ICarProtoServer
    {
        IEnumerable<ICarProtocol> GetConnections();
        ICarProtocol GetConnection(int id);
    }
}