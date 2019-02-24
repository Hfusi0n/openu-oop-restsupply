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
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly RestSupplyDB.RestSupplyDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserDashboardController()
        {
            _dbContext = new RestSupplyDB.RestSupplyDbContext();
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult _SystemUserEdit(string id)
        {
            // Get all roles from the database
            var user = _unitOfWork.Account.GetById(id);            
            AppUserRole userRole = user.Role;

            UserViewModel model = new UserViewModel
            {
                Id = user.UserId,
                Email = user.Email,
                PrivateName = user.FirstName,
                LastName = user.LastName                
            };

            // Search if the user got an assigned role
            var roleId = user.Role?.RoleId;

            AppRole selectedRole = null;
            if(roleId != null)
            {
               selectedRole = _dbContext.Roles.FirstOrDefault(x => x.Id == roleId);
            }

            // Get the user role if exist
            model.SelectedUserRole = selectedRole?.Name;

            // Get all roles
            model.RoleList = _unitOfWork.Account.GetAppRoles();

            return View(model);
        }

        //
        // POST: Update the user data
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserProfile(UserViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                // Get the current application user
                //AppUser user = _unitOfWork.Account.GetById(model.Id);
                // Temporary
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == model.Id);
                // Create a user manager
                AppUserManager userManager = new AppUserManager(new AppUserStore(_dbContext));

                if (!string.IsNullOrEmpty(model.SelectedUserRole))
                {
                    // Add the new role if needed
                    if (user != null)
                    {
                        var roleResult = userManager.AddToRole(user.Id, model.SelectedUserRole);
                    }
                }

                // Update the user
                if (user != null)
                {
                    user.FirstName = model.PrivateName;
                    user.LastName = model.LastName;

                    // TODO consider overriding UpdateAsync so it passes AppUserDTO and not AppUser
                    var userResult = await userManager.UpdateAsync(user);
                }
            }

            return RedirectToAction("Admin");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult _SystemUsersList()
        {
            var users = _unitOfWork.Account.GetAll();
            
            UsersListViewModel usersListViewModel = 
                new UsersListViewModel();
            
            List<UserViewModel> usersList = 
                new List<UserViewModel>();

            foreach (var user in users)
            {
                // Create a new view model for the user
                // and push the user database data to the viewmodel
                UserViewModel userViewModel =
                    new UserViewModel
                    {
                        Id = user.UserId,
                        Email = user.Email,
                        PrivateName = user.FirstName,
                        LastName = user.LastName
                };

                usersList.Add(userViewModel);
            }

            usersListViewModel.UsersEnumerable = usersList;

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
                    return RedirectToAction("Admin", new { id = user.Id });
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