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

        public float Temp1 { get { return FloatSensorValues.LastOrDefault(c => c.SensorName == "TEMP1")?.Value ?? 0; } }
        public float Voltage { get { return FloatSensorValues.LastOrDefault(c => c.SensorName == "VOLTAGE")?.Value ?? 0; } }
        public float Speed { get { return FloatSensorValues.LastOrDefault(c => c.SensorName == "SPEED")?.Value ?? 0; } }

        public ICollection<FloatSensorValue> FloatSensorValues { get; set; }
        public ICollection<GpsLocation> GpsLocations { get; set; }
        public ICollection<GSensor> GSensors { get; set; }
        public ICollection<Sms> Smses { get; set; } 
    }
}
