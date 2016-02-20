using System;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class GpsCommand : IInputCommand
    {
        private readonly ICarService _carService;
        private readonly int _carId;
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly DateTime _time;

        public GpsCommand(ICarService carService, int carId, double latitude, double longitude,
            DateTime time)
        {
            _carService = carService;
            _carId = carId;
            _latitude = latitude;
            _longitude = longitude;
            _time = time;
        }

        public void Execute()
        {
            var location = new GpsLocation
            {
                CarId = _carId,
                Latitude = _latitude,
                Longitude = _longitude,
                Time = _time
            };
            _carService.GetCar(_carId).GpsLocations.Add(location);
            _carService.SaveCar();
        }
    }
}
