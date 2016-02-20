using System;
using CarControl.CarConnect.InputCommands;
using CarControl.Service;

namespace CarControl.CarConnect.CommandsCommon
{
    public interface ICommandFactory
    {
        IInputCommand CreateTemp1(int carId, float temperature, DateTime time);
        IInputCommand CreateVoltage(int carId, float voltage, DateTime time);
        IInputCommand CreateGSensor(int carId, int x, int y, int z, DateTime time);
        IInputCommand CreateGps(int carId, double latitude, double longitude, DateTime time);
        IInputCommand CreateSmsIn(int carId, string text, DateTime time);
        IInputCommand CreateSmsOut(int carId, string text, DateTime time);
        IInputCommand CreateSpeed(int carId, int speed, DateTime time);
    }

    public class InputCommandFactory : ICommandFactory
    {
        public ICarService CarService { get; }
        
        public InputCommandFactory(ICarService carService)
        {
            CarService = carService;
        }
        
        public IInputCommand CreateTemp1(int carId, float temperature, DateTime time)
        {
            return new Temp1Command(CarService, carId, temperature, time);
        }

        public IInputCommand CreateVoltage(int carId, float voltage, DateTime time)
        {
            return new VoltageCommand(CarService, carId, voltage, time);
        }

        public IInputCommand CreateGSensor(int carId, int x, int y, int z, DateTime time)
        {
            return new GSensorCommand(CarService, carId, x, y, z, time);
        }

        public IInputCommand CreateGps(int carId, double latitude, double longitude, DateTime time)
        {
            return new GpsCommand(CarService, carId, latitude, longitude, time);
        }

        public IInputCommand CreateSmsIn(int carId, string text, DateTime time)
        {
            return new SmsInCommand(CarService, carId, text, time);
        }

        public IInputCommand CreateSmsOut(int carId, string text, DateTime time)
        {
            return new SmsOutCommand(CarService, carId, text, time);
        }

        public IInputCommand CreateSpeed(int carId, int speed, DateTime time)
        {
            return new SpeedCommand(CarService, carId, speed, time);
        }
    }
}
