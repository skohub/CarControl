using System;

namespace CarConnect.Model
{
    public class GpsLocation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }
    }
}
