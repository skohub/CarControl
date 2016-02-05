using Autofac;
using CarConnect.Data.Infrastructure;

namespace CarConnect.Test
{
    public class TestBase
    {
        private IContainer _container;

        protected IContainer AutofacContainer
        {
            get
            {
                if (_container != null) return _container;
                var builder = new ContainerBuilder();
                builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();//.InstancePerRequest();
                builder.RegisterType<DbFactory>().As<IDbFactory>();//.InstancePerRequest();
                _container = builder.Build();
                return _container;
            }
        }
    }
}
