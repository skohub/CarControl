using System;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class VoltageCommand : IInputCommand
    {
        private readonly ICarService _carService;
        private readonly int _carId;
        private readonly float _voltage;
        private readonly DateTime _time;

        public VoltageCommand(ICarService carService, int carId, float voltage, DateTime time)
        {
            _carService = carService;
            _carId = carId;
            _voltage = voltage;
            _time = time;
        }

        public void Execute()
        {
            var value = new FloatSensorValue {SensorName = "VOLTAGE", CarId = _carId, Value = _voltage, Time = _time};
            _carService.GetCar(_carId).FloatSensorValues.Add(value);
            _carService.SaveCar();
        }
    }
}
