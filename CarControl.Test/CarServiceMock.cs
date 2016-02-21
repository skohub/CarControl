using System;
using System.Collections.Generic;
using CarConnect.Model;
using CarControl.Service;

namespace CarConnect.Test
{
    public class CarServiceMock : ICarService
    {
        public IEnumerable<Car> GetCars()
        {
            var list = new List<Car>();
            var car = new Car
            {
                CarId = 1,
                Hash = "123",
                Imei = "123456789012345",
                Name = "Car 1"
            };
            list.Add(car);
            return list;
        }

        public Car GetCar(int id)
        {
            var car = new Car
            {
                CarId = 1,
                Hash = "123",
                Imei = "123456789012345",
                Name = "Car 1"
            };
            return car;
        }

        public Car GetCarByImei(string imei)
        {
            var car = new Car
            {
                CarId = 1,
                Hash = "123",
                Imei = "123456789012345",
                Name = "Car 1"
            };
            return car;
        }

        public void SaveCar()
        {
            throw new NotImplementedException();
        }

        public void CreateCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
