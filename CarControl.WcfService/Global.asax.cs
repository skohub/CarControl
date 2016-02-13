namespace CarControl.WcfService
{
    public class WcfServiceApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
        }
    }
}
