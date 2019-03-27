using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly RestSupplyDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserDashboardController()
        {
            _dbContext = new RestSupplyDbContext();
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult _SystemUserEdit(string id)
        {
            // Get all roles from the database
            var user = _unitOfWork.Users.GetById(id);            
            var userRoles = user.Roles;
            UserViewModel model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PrivateName = user.FirstName,
                LastName = user.LastName                
            };

            // Get all roles
            model.AllRolesList = _unitOfWork.Roles.GetAll().Select(r => new RoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            });

            return View(model);
        }

        // POST: Update the user data
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public  ActionResult UpdateUserProfile(UserViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                // Get the current application user
                var user = _unitOfWork.Users.GetById(model.Id);
                // Create a user manager
                AppUserManager userManager = new AppUserManager(new AppUserStore(_dbContext));

                if (!string.IsNullOrEmpty(model.SelectedUserRole))
                {
                    // Add the new role if needed
                    if (user != null)
                    {
                        userManager.AddToRole(user.Id, model.SelectedUserRole);
                    }
                }

                // Update the user
                if (user != null)
                {
                    user.FirstName = model.PrivateName;
                    user.LastName = model.LastName;

                    userManager.Update(user);
                }
            }

            return RedirectToAction("Admin");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult _SystemUsersList()
        {
            var users = _unitOfWork.Users.GetAll().ToList();
            var usersListViewModel = new UsersListViewModel
            {
                UsersList = new List<UserViewModel>()
            };

            foreach (var user in users)
            {
                var userRolesVm = user.Roles.ToList().Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    RoleName = _unitOfWork.Roles.GetById(r.RoleId).Name
                });
                usersListViewModel.UsersList.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    PrivateName = user.FirstName,
                    LastName = user.LastName,
                    UserRolesList = userRolesVm
                });
            }
            return View(usersListViewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("SearchUsers")]
        public async Task<ActionResult> SearchUsers(UserViewModel model)
        {
            AppUser user = null;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Email))
                {
                    // Create a user manager
                    var userManager = new AppUserManager(new AppUserStore(_dbContext));
                    user = await userManager.FindByEmailAsync(model.Email);
                }

                if (user != null)
                {
                    var userVm = new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        PrivateName = user.FirstName,
                        LastName = user.LastName
                    };
                    return View("_SystemUserEdit", userVm);
                }
            }


            return RedirectToAction("Admin");
        }

        // GET: UserDashboard
        [Authorize(Roles = "Admin")]
        public ActionResult Admin(string id)
        {
            UserViewModel model = new UserViewModel
            {
                Id = id
            };
            
            return View(model);
        }
    }
}