using System;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class Temp1Command : IInputCommand
    {
        private readonly ICarService _carService;
        private readonly int _carId;
        private readonly float _temperature;
        private readonly DateTime _time;

        public Temp1Command(ICarService carService, int carId, float temperature, DateTime time)
        {
            _carService = carService;
            _carId = carId;
            _temperature = temperature;
            _time = time;
        }

        public void Execute()
        {
            var car = _carService.GetCar(_carId);
            var value = new FloatSensorValue { SensorName = "TEMP1", Time = _time, Value = _temperature };
            car.FloatSensorValues.Add(value);
            _carService.SaveCar();
        }
    }
}
