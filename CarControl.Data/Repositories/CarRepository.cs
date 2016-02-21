﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Data.Infrastructure;
using CarConnect.Model;

namespace CarConnect.Data.Repositories
{
    public interface ICarRepository : IRepository<Car>
    {
        Car GetCarByImei(string imei);
    }

    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public new Car GetById(int id)
        {
            var car = base.GetById(id);
            DbContext.Entry(car)
                .Collection(b => b.FloatSensorValues)
                .Query()
                .OrderByDescending(b => b.Id)
                .Take(10)
                .Load();
            return car;
        }

        public Car GetCarByImei(string imei)
        {
            var car = DbContext.Cars.FirstOrDefault(c => c.Imei == imei);
            DbContext.Entry(car)
                .Collection(b => b.FloatSensorValues)
                .Query()
                .OrderByDescending(b => b.Id)
                .Take(10)
                .Load();
            return car;
        }
    }
}
