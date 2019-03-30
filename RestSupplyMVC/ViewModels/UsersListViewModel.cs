using RestSupplyDB.Models.AppUser;
using System;
using System.Collections;
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
        public string SelectedUserRole { get; set; }
        public string RoleName  { get; set; }
        public List<RoleViewModel> UserRolesList { get; set; }
        public List<RoleViewModel> AllRolesList { get; set; }
        public string UserRolesAsString => (UserRolesList == null || !UserRolesList.Any()) ? "" : (string.Join(", ", UserRolesList.Select(r => r.RoleName)));
        public string[] UpdatedUserRoleNamesArr { get; set; }
        public string[] CurrentUserRoleNamesArr { get
        {
            return  (UserRolesList == null || !UserRolesList.Any()) ? new string[]{} : UserRolesList.Select(r => r.RoleName)
                .ToArray();
        } }
    }

    public class UsersListViewModel
    {
        public List<UserViewModel> UsersList { get; set; }
    }
}