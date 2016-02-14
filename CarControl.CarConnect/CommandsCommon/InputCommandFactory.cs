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
        IInputCommand CreateSmsCommit();
    }

    public class InputCommandFactory : ICommandFactory
    {
        private readonly ISmsService _smsService;
        public ICarService CarSevice { get; }
        public ISensorService SensorService { get; }
        
        public InputCommandFactory(ICarService carService, ISensorService sensorService, ISmsService smsService)
        {
            _smsService = smsService;
            CarSevice = carService;
            SensorService = sensorService;
        }
        
        public IInputCommand CreateTemp1(int carId, float temperature, DateTime time)
        {
            return new Temp1Command(SensorService, carId, temperature, time);
        }

        public IInputCommand CreateVoltage(int carId, float voltage, DateTime time)
        {
            return new VoltageCommand(SensorService, carId, voltage, time);
        }

        public IInputCommand CreateGSensor(int carId, int x, int y, int z, DateTime time)
        {
            return new GSensorCommand(carId, SensorService, x, y, z, time);
        }

        public IInputCommand CreateGps(int carId, double latitude, double longitude, DateTime time)
        {
            return new GpsCommand(carId, SensorService, latitude, longitude, time);
        }

        public IInputCommand CreateSmsIn(int carId, string text, DateTime time)
        {
            return new SmsInCommand(_smsService, carId, text, time);
        }

        public IInputCommand CreateSmsOut(int carId, string text, DateTime time)
        {
            return new SmsOutCommand(_smsService, carId, text, time);
        }

        public IInputCommand CreateSpeed(int carId, int speed, DateTime time)
        {
            return new SpeedCommand(SensorService, carId, speed, time);
        }

        public IInputCommand CreateSmsCommit()
        {
            return new SmsCommitCommand(_smsService);
        }
    }
}
