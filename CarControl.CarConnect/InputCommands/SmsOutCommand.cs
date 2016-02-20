using System;
using CarConnect.Model;
using CarControl.CarConnect.CommandsCommon;
using CarControl.Service;

namespace CarControl.CarConnect.InputCommands
{
    public class SmsOutCommand : IInputCommand
    {
        private readonly ICarService _carService;
        private readonly int _carId;
        private readonly string _text;
        private readonly DateTime _time;

        public SmsOutCommand(ICarService carService, int carId, string text, DateTime time)
        {
            _carService = carService;
            _carId = carId;
            _text = text;
            _time = time;
        }

        public void Execute()
        {
            var sms = new Sms {Direction = "OUT", CarId = _carId, Text = _text, Time = _time};
            _carService.GetCar(_carId).Smses.Add(sms);
            _carService.SaveCar();
        }
    }
}
