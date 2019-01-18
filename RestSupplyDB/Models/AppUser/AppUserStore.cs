using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RestSupplyDB.Models.AppUser
{
    class AppUserStore : UserStore<AppUser, AppRole, string,
        AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(RestSupplyDbContext context)
            : base(context)
        {
        }
    }
}
