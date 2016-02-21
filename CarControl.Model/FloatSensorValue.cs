using System;

namespace CarConnect.Model
{
    public class FloatSensorValue
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string SensorName { get; set; }
        public float Value { get; set; }
        public DateTime Time { get; set; }
    }
}
