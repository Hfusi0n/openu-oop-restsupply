using System.Collections.Generic;
using RestSupplyDB.Models.Kitchen;

namespace RestSupplyDB.Models.AppUser
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class AppUser : IdentityUser<string, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AppUser()
        {
            UserKitchens = new HashSet<KitchenUsers>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<KitchenUsers> UserKitchens { get; set; }
    }
}  
