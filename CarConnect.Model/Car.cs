using System.Collections.Generic;
using System.Linq;

namespace CarConnect.Model
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Imei { get; set; }
        public string Hash { get; set; }

        public virtual List<FloatSensorValue> FloatSensorValues { get; set; }
        public virtual List<GpsLocation> GpsLocations { get; set; }
        public virtual List<GSensor> GSensors { get; set; }
        public virtual List<Sms> Smses { get; set; } 
    }
}
