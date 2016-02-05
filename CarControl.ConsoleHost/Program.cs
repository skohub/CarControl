using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Autofac.Integration.Wcf;
using CarControl.Contract;
using CarControl.Service;

namespace CarControl.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = AutofacConfig.ConfigureContainer();
            using (var container = builder.Build())
            {
                var address = new Uri("http://localhost:4998");
                var host = new ServiceHost(typeof(CarService), address);
                host.AddServiceEndpoint(typeof(ICarService), new BasicHttpBinding(), string.Empty);
                host.AddDependencyInjectionBehavior<ICarService>(container);
                host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpGetUrl = address });
                host.Open();

                Console.WriteLine("Service running");
                Console.ReadLine();

                host.Close();
                Environment.Exit(0);
            }
        }
    }
}
