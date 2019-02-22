using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using RestSupplyDB.Models.Kitchen;
using RestSupplyDB.Models.AppUser;


namespace RestSupplyMVC.DTOs
{
    public class AppUserDTO
    {
        public string UserId { get; set; }
        public AppUserRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<KitchenUsers> Kitchens { get; set; }

    }

}