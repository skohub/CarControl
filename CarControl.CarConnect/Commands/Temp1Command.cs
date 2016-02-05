using System;
using CarConnect.Model;
using CarControl.Contract;

namespace CarControl.CarConnect.Commands
{
    public class Temp1Command : ICommand
    {
        private readonly ISensorService _sensorService;
        private readonly int _carId;
        private readonly float _temperature;
        private readonly DateTime _time;

        public Temp1Command(ISensorService sensorService, int carId, float temperature, DateTime time)
        {
            _sensorService = sensorService;
            _carId = carId;
            _temperature = temperature;
            _time = time;
        }

        public bool Execute()
        {
            var value = new FloatSensorValue {SensorName = "TEMP1", CarId = _carId, Time = _time, Value = _temperature};
            _sensorService.RegisterValue(value);
            return true;
        }
    }
}
