using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestSupplyMVC.Startup))]
namespace RestSupplyMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}