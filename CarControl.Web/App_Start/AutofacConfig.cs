using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using CarConnect.Data.Infrastructure;
using CarConnect.Data.Repositories;
using CarControl.CarConnect.Commands;
using CarControl.CarConnect.Server;
using CarControl.Service;

namespace CarControl.Web
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // Create your builder.
            var builder = new ContainerBuilder();
            // You can register controllers all at once using assembly scanning...
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<DbFactory>().As<IDbFactory>().SingleInstance();//.InstancePerRequest();
            
            // Repositories
            builder.RegisterAssemblyTypes(typeof (CarRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().SingleInstance();//.InstancePerRequest();

            //Services
            builder.RegisterAssemblyTypes(typeof(CarService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().SingleInstance();//.InstancePerRequest();

            builder.RegisterType<TextCommandFactory>().As<ICommandFactory>();
            builder.RegisterType<CarProtoServer>().As<ICarProtoServer>()
                .SingleInstance()
                .AutoActivate();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}