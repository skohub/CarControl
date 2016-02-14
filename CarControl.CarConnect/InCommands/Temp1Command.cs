using System;
using CarConnect.Model;
using CarControl.Service;

namespace CarControl.CarConnect.InCommands
{
    public class Temp1Command : IInCommand
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

        public void Execute()
        {
            var value = new FloatSensorValue {SensorName = "TEMP1", CarId = _carId, Time = _time, Value = _temperature};
            _sensorService.RegisterValue(value);
        }
    }
}
