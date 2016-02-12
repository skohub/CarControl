using System;
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
                var host = new ServiceHost(typeof(CarCommand), address);
                host.AddServiceEndpoint(typeof(ICarCommand), new WSDualHttpBinding(), string.Empty);
                host.AddDependencyInjectionBehavior<CarCommand>(container);
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
