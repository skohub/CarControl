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

        public FloatSensorValue Temp1
        {
            get { return FloatSensorValues.LastOrDefault(c => c.SensorName == "TEMP1"); }
            set { FloatSensorValues.Add(value); }
        }

        public virtual List<FloatSensorValue> FloatSensorValues { get; set; }
    }
}
