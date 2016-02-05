using System;
using CarConnect.Model;
using CarControl.Contract;

namespace CarControl.CarConnect.Commands
{
    public class SpeedCommand : ICommand
    {
        private readonly ISensorService _sensorService;
        private readonly int _carId;
        private readonly int _speed;
        private readonly DateTime _time;

        public SpeedCommand(ISensorService sensorService, int carId, int speed, DateTime time)
        {
            _sensorService = sensorService;
            _carId = carId;
            _speed = speed;
            _time = time;
        }

        public bool Execute()
        {
            var value = new FloatSensorValue {SensorName = "SP", CarId = _carId, Value = _speed, Time = _time};
            _sensorService.RegisterValue(value);
            return true;
        }
    }
}
