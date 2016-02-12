using System;
using CarConnect.Model;
using CarControl.Service;

namespace CarControl.CarConnect.Commands
{
    public class SpeedCommand : IInputCommand
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

        public void Execute()
        {
            var value = new FloatSensorValue {SensorName = "SP", CarId = _carId, Value = _speed, Time = _time};
            _sensorService.RegisterValue(value);
        }
    }
}
