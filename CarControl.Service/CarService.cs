using System.Collections.Generic;
using CarConnect.Data.Infrastructure;
using CarConnect.Data.Repositories;
using CarConnect.Model;

namespace CarControl.Service
{
    public interface ICarService
    {
        IEnumerable<Car> GetCars();
        Car GetCar(int id);
        Car GetCarByImei(string imei);
        void CreateCar(Car car);
        void SaveCar();
    }

    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CarService(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        #region ICarService members

        public IEnumerable<Car> GetCars()
        {
            var cars = _carRepository.GetAll();
            return cars;
        }

        public Car GetCar(int id)
        {
            var car = _carRepository.GetById(id);
            return car;
        }

        public Car GetCarByImei(string imei)
        {
            var car = _carRepository.GetCarByImei(imei);
            return car;
        }

        public void CreateCar(Car car)
        {
            _carRepository.Add(car);
        }

        public void SaveCar()
        {
            _unitOfWork.Commit();
        }

        #endregion

    }
}
