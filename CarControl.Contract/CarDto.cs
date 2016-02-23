using System.Collections.Generic;

namespace CarControl.Contract
{
    public class CarDto
    {
        public int CarId { get; set; }
        public int ConnectionId { get; set; }
        public string Name { get; set; }
        public string Imei { get; set; }
        public string Hash { get; set; }
        public float Temp1 { get; set; }
        public float Speed { get; set; }
        public float Voltage { get; set; }
        //public List<double> GpsCoordinates { get; set; }
        //public List<double> GSensorValues { get; set; }
    }
}
