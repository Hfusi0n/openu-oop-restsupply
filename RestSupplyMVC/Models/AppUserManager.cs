using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RestSupplyDB;
using RestSupplyDB.Models;

namespace RestSupplyMVC.Models
{
    public class AppUserManager : UserManager<UsersSet>
    {
        public AppUserManager(IUserStore<UsersSet> store)
            : base(store)
        {
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(
                new UserStore<UsersSet>(context.Get<RestSupplyDBModel>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}