using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;

namespace CarControl.Contract
{
    [ServiceContract]
    public interface ICarService
    {
        [OperationContract]
        IEnumerable<Car> GetCars();

        [OperationContract]
        Car GetCar(int id);

        [OperationContract]
        Car GetCarByImei(string imei);

        [OperationContract]
        void CreateCar(Car car);

        [OperationContract]
        void SaveCar();
    }
}
