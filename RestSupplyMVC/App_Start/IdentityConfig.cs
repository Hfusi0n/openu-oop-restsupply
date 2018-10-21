using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin;
using RestSupplyDB;
using RestSupplyDB.Models;
using RestSupplyMVC.Models;
using Microsoft.AspNet.Identity.Owin;

namespace RestSupplyMVC
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new RestSupplyDBModel());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<RoleManager<RolesSet>>((options, context) =>
                new RoleManager<RolesSet>(
                    new RoleStore<RolesSet>(context.Get<RestSupplyDBModel>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}