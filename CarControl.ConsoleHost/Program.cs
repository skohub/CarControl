using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using Autofac;
using Autofac.Integration.Wcf;
using CarControl.CarConnect.CommandsCommon;
using CarControl.CarConnect.Protocol;
using CarControl.CarConnect.Server;
using CarControl.Contract;

namespace CarControl.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = AutofacConfig.ConfigureContainer();
            using (var container = builder.Build())
            {
                // Car commands
                var carProtoServer = container.Resolve<ICarProtoServer>();
                carProtoServer.GetInputCommandFactory = () =>
                {
                    var scope = container.BeginLifetimeScope();
                    carProtoServer.OnClientDisconnected = () => scope.Dispose();
                    return scope.Resolve<ICommandFactory>();
                };

                // Wcf
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
