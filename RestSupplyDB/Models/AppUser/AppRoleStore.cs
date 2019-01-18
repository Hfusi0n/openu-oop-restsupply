using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RestSupplyDB.Models.AppUser
{
    public class AppRoleStore : RoleStore<AppRole, string, AppUserRole>
    {
        public AppRoleStore(RestSupplyDbContext context)
            : base(context)
        {
        }
    }
}
