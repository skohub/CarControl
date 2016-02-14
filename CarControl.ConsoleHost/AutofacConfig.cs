using Autofac;
using CarConnect.Data.Infrastructure;
using CarConnect.Data.Repositories;
using CarControl.CarConnect.CommandsCommon;
using CarControl.CarConnect.Protocol;
using CarControl.CarConnect.Server;
using CarControl.Service;

namespace CarControl.ConsoleHost
{
    public class AutofacConfig
    {
        public static ContainerBuilder ConfigureContainer()
        {
            // Create your builder.
            var builder = new ContainerBuilder();
            // You can register controllers all at once using assembly scanning...
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerLifetimeScope();
            
            // Repositories
            builder.RegisterAssemblyTypes(typeof (CarRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //Services
            builder.RegisterAssemblyTypes(typeof(CarService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<InputCommandFactory>().As<ICommandFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CarProtoServer>().As<ICarProtoServer>()
                .SingleInstance()
                .AutoActivate();
            builder.RegisterType<CarCommand>();

            return builder;
        }
    }
}