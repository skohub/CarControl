using System;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class SpeedCommand : IInputCommand
    {
        private readonly ICarService _carService;
        private readonly int _carId;
        private readonly int _speed;
        private readonly DateTime _time;

        public SpeedCommand(ICarService carService, int carId, int speed, DateTime time)
        {
            _carService = carService;
            _carId = carId;
            _speed = speed;
            _time = time;
        }

        public void Execute()
        {
            var value = new FloatSensorValue {SensorName = "SP", CarId = _carId, Value = _speed, Time = _time};
            _carService.GetCar(_carId).FloatSensorValues.Add(value);
            _carService.SaveCar();
        }
    }
}
