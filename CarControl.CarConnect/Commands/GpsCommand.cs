using System;
using CarConnect.Model;
using CarControl.Contract;

namespace CarControl.CarConnect.Commands
{
    public class GpsCommand : ICommand
    {
        private readonly int _carId;
        private readonly ISensorService _sensorService;
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly DateTime _time;

        public GpsCommand(int carId, ISensorService sensorService, double latitude, double longitude,
            DateTime time)
        {
            _carId = carId;
            _sensorService = sensorService;
            _latitude = latitude;
            _longitude = longitude;
            _time = time;
        }

        public bool Execute()
        {
            var location = new GpsLocation
            {
                CarId = _carId,
                Latitude = _latitude,
                Longitude = _longitude,
                Time = _time
            };
            _sensorService.RegisterLocation(location);
            return true;
        }
    }
}
