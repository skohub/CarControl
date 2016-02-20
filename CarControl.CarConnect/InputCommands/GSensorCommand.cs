using System;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class GSensorCommand : IInputCommand
    {
        private readonly ICarService _carService;
        private readonly int _carId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly DateTime _time;

        public GSensorCommand(ICarService carService, int carId, int x, int y, int z, DateTime time)
        {
            _carService = carService;
            _carId = carId;
            _x = x;
            _y = y;
            _z = z;
            _time = time;
        }

        public void Execute()
        {
            var gsensor = new GSensor {CarId = _carId, X = _x, Y = _y, Z = _z, Time = _time };
            _carService.GetCar(_carId).GSensors.Add(gsensor);
            _carService.SaveCar();
        }
    }
}
