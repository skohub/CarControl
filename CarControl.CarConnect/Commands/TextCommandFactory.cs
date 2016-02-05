using System;
using CarControl.CarConnect.Protocol;
using CarControl.Contract;

namespace CarControl.CarConnect.Commands
{
    public interface ICommandFactory
    {
        ICommand CreateCommandMode(ICarProtocol carProtocol, string parameter);
        ICommand CreateTemp1(int carId, float temperature, DateTime time);
        ICommand CreateVoltage(int carId, float voltage, DateTime time);
        ICommand CreateGSensor(int carId, int x, int y, int z, DateTime time);
        ICommand CreateGps(int carId, double latitude, double longitude, DateTime time);
        ICommand CreateSmsIn(int carId, string text, DateTime time);
        ICommand CreateSmsOut(int carId, string text, DateTime time);
        ICommand CreateSpeed(int carId, int speed, DateTime time);
        ICommand CreateSmsCommit();
    }

    public class TextCommandFactory : ICommandFactory
    {
        private readonly ISmsService _smsService;
        public ICarService CarSevice { get; }
        public ISensorService SensorService { get; }
        
        public TextCommandFactory(ICarService carService, ISensorService sensorService, ISmsService smsService)
        {
            _smsService = smsService;
            CarSevice = carService;
            SensorService = sensorService;
        }

        public ICommand CreateCommandMode(ICarProtocol carProtocol, string parameter)
        {
            return new CommandModeCommand(carProtocol, parameter);
        }

        public ICommand CreateTemp1(int carId, float temperature, DateTime time)
        {
            return new Temp1Command(SensorService, carId, temperature, time);
        }

        public ICommand CreateVoltage(int carId, float voltage, DateTime time)
        {
            return new VoltageCommand(SensorService, carId, voltage, time);
        }

        public ICommand CreateGSensor(int carId, int x, int y, int z, DateTime time)
        {
            return new GSensorCommand(carId, SensorService, x, y, z, time);
        }

        public ICommand CreateGps(int carId, double latitude, double longitude, DateTime time)
        {
            return new GpsCommand(carId, SensorService, latitude, longitude, time);
        }

        public ICommand CreateSmsIn(int carId, string text, DateTime time)
        {
            return new SmsInCommand(_smsService, carId, text, time);
        }

        public ICommand CreateSmsOut(int carId, string text, DateTime time)
        {
            return new SmsOutCommand(_smsService, carId, text, time);
        }

        public ICommand CreateSpeed(int carId, int speed, DateTime time)
        {
            return new SpeedCommand(SensorService, carId, speed, time);
        }

        public ICommand CreateSmsCommit()
        {
            return new SmsCommitCommand(_smsService);
        }
    }
}
