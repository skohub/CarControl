using System;
using CarControl.CarConnect.Protocol;
using CarControl.Service;

namespace CarControl.CarConnect.InCommands
{
    public interface ICommandFactory
    {
        IInCommand CreateTemp1(int carId, float temperature, DateTime time);
        IInCommand CreateVoltage(int carId, float voltage, DateTime time);
        IInCommand CreateGSensor(int carId, int x, int y, int z, DateTime time);
        IInCommand CreateGps(int carId, double latitude, double longitude, DateTime time);
        IInCommand CreateSmsIn(int carId, string text, DateTime time);
        IInCommand CreateSmsOut(int carId, string text, DateTime time);
        IInCommand CreateSpeed(int carId, int speed, DateTime time);
        IInCommand CreateSmsCommit();
    }

    public class InCommandFactory : ICommandFactory
    {
        private readonly ISmsService _smsService;
        public ICarService CarSevice { get; }
        public ISensorService SensorService { get; }
        
        public InCommandFactory(ICarService carService, ISensorService sensorService, ISmsService smsService)
        {
            _smsService = smsService;
            CarSevice = carService;
            SensorService = sensorService;
        }
        
        public IInCommand CreateTemp1(int carId, float temperature, DateTime time)
        {
            return new Temp1Command(SensorService, carId, temperature, time);
        }

        public IInCommand CreateVoltage(int carId, float voltage, DateTime time)
        {
            return new VoltageCommand(SensorService, carId, voltage, time);
        }

        public IInCommand CreateGSensor(int carId, int x, int y, int z, DateTime time)
        {
            return new GSensorCommand(carId, SensorService, x, y, z, time);
        }

        public IInCommand CreateGps(int carId, double latitude, double longitude, DateTime time)
        {
            return new GpsCommand(carId, SensorService, latitude, longitude, time);
        }

        public IInCommand CreateSmsIn(int carId, string text, DateTime time)
        {
            return new SmsInCommand(_smsService, carId, text, time);
        }

        public IInCommand CreateSmsOut(int carId, string text, DateTime time)
        {
            return new SmsOutCommand(_smsService, carId, text, time);
        }

        public IInCommand CreateSpeed(int carId, int speed, DateTime time)
        {
            return new SpeedCommand(SensorService, carId, speed, time);
        }

        public IInCommand CreateSmsCommit()
        {
            return new SmsCommitCommand(_smsService);
        }
    }
}
