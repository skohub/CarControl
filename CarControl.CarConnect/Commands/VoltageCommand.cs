using System;
using CarConnect.Model;
using CarControl.Contract;

namespace CarControl.CarConnect.Commands
{
    public class VoltageCommand : IInputCommand
    {
        private readonly ISensorService _sensorService;
        private readonly int _carId;
        private readonly float _voltage;
        private readonly DateTime _time;

        public VoltageCommand(ISensorService sensorService, int carId, float voltage, DateTime time)
        {
            _sensorService = sensorService;
            _carId = carId;
            _voltage = voltage;
            _time = time;
        }

        public void Execute()
        {
            var value = new FloatSensorValue {SensorName = "VOLTAGE", CarId = _carId, Value = _voltage, Time = _time};
            _sensorService.RegisterValue(value);
        }
    }
}
