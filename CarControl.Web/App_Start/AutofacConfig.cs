using System.ServiceModel;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using CarControl.Contract;
using CarControl.Web.WcfProxy;

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
            builder.Register(c => new CarCommandProxy(new InstanceContext(new CarCommandCallback()))).As<ICarCommand>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}