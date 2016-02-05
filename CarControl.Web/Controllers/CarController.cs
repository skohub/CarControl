using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CarControl.CarConnect.Protocol;
using CarControl.CarConnect.Server;
using CarControl.Contract;
using CarControl.Web.Models;
using NLog;
using NLog.Targets;

namespace CarControl.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICarProtoServer _srv;

        public CarController(ICarProtoServer srv, ICarService carService)
        {
            _srv = srv;
            _carService = carService;
        }

        // GET: Car
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var cars = _srv.GetConnections();
            //var cars = _carService.GetCars();
            var viewModelCars = Mapper.Map<IEnumerable<ICarProtocol>, IEnumerable<CarViewModel>>(cars);
            return View(viewModelCars);
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
            var car = _srv.GetConnection(id);
            car.Send("PING");
            return View();
        }
    }
}