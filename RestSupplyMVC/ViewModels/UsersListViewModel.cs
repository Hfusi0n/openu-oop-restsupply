using RestSupplyDB.Models.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSupplyMVC.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PrivateName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }

        public string FullName => PrivateName + " " + LastName;
        public string SelectedUserRole { get; set; }
        public IEnumerable<AppRole> RoleList { get; set; }
    }

    public class UsersListViewModel
    {
        public IEnumerable<UserViewModel> UsersEnumerable { get; set; }
    }
}