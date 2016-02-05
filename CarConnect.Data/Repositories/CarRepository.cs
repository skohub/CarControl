using System;
using System.Collections.Generic;
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

        public Car GetCarByImei(string imei)
        {
            var car = DbContext.Cars.FirstOrDefault(c => c.Imei == imei);
            return car;
        }
    }
}
