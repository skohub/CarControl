using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CarControl.Contract;
using CarControl.Web.Models;
using NLog;
using NLog.Targets;

namespace CarControl.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarCommand _carCommand;

        public CarController(ICarCommand carCommand)
        {
            _carCommand = carCommand;
        }

        // GET: Car
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var connections = _carCommand.ConnectionList();
            var cars = new List<CarDto>();
            foreach (var i in connections)
            {
                cars.Add(_carCommand.GetCar(i));
            }
            //var viewModelCars = Mapper.Map<IEnumerable<ICarProtocol>, IEnumerable<CarViewModel>>(cars);
            return View(cars);
        }

        public ActionResult Log()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var target = (MemoryTarget) LogManager.Configuration.FindTargetByName("m");
            var skipCount = Math.Max(0, target.Logs.Count - 100);
            return View(target.Logs.Skip(skipCount).Reverse());
        }

        public ActionResult Ping(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            _carCommand.Ping(1);
            return View();
        }
    }
}