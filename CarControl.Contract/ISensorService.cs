using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;

namespace CarControl.Contract
{
    [ServiceContract]
    public interface ISensorService
    {
        [OperationContract]
        void RegisterValue(FloatSensorValue sensor);

        [OperationContract]
        void RegisterLocation(GpsLocation location);

        [OperationContract]
        void RegisterGSensor(GSensor gsensor);
    }
}
