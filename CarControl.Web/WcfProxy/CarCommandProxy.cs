using System.Collections.Generic;
using System.ServiceModel;
using CarControl.Contract;

namespace CarControl.Web.WcfProxy
{
    public class CarCommandProxy : ClientBase<ICarCommand>, ICarCommand
    {
        public CarCommandProxy(InstanceContext callbackInstance) : base(callbackInstance) { }

        public void Ping(int id)
        {
            Channel.Ping(id);
        }

        public List<int> ConnectionList()
        {
            return Channel.ConnectionList();
        }
    }
}