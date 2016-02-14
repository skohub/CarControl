using System;
using CarConnect.Model;
using CarControl.Service;

namespace CarControl.CarConnect.InCommands
{
    public class GSensorCommand : IInCommand
    {
        private readonly int _carId;
        private readonly ISensorService _sensorService;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly DateTime _time;

        public GSensorCommand(int carId, ISensorService sensorService, int x, int y, int z, DateTime time)
        {
            _carId = carId;
            _sensorService = sensorService;
            _x = x;
            _y = y;
            _z = z;
            _time = time;
        }

        public void Execute()
        {
            var gsensor = new GSensor() {CarId = _carId, X = _x, Y = _y, Z = _z, Time = _time };
            _sensorService.RegisterGSensor(gsensor);
        }
    }
}
