using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarControl.Web.Startup))]
namespace CarControl.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
