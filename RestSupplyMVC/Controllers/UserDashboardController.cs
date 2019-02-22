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

        [Authorize]
        public ActionResult _SystemUserEdit(ViewModels.UserViewModel model)
        {
            // Get all roles from the database
            var user = _unitOfWork.Account.GetById(model.Id);
            AppUserRole userRole = user.Role;

            if(userRole == null)
            {
                model.SelectedUserRole = null;
            }
            else
            {
                var roleId = user.Role.RoleId;

                model.SelectedUserRole = _dbContext.Roles.FirstOrDefault(x => x.Id == roleId)?.Name;
            }

            model.RoleList = _unitOfWork.Account.GetAppRoles();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ActionName("SearchUsers")]
        public async Task<ActionResult> SearchUsers(ViewModels.UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(!string.IsNullOrEmpty(model.Email))
                {
                    // Create a user manager
                    var userManager = new AppUserManager(new AppUserStore(_dbContext));
                    userManager.FindByEmailAsync("");
                }                
            }

            return View();
        }

        //
        // POST: Update the user data
        [HttpPost]
        [Authorize]
        [ActionName("UpdateUserProfile")]
        public async Task<ActionResult> UpdateUserProfile(ViewModels.UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the current application user
                //var user = _unitOfWork.Account.GetById(model.Id);
                // Temporary
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == model.Id);

                // Create a user manager
                AppUserManager userManager = new AppUserManager(new AppUserStore(_dbContext));

                if (user != null)
                {
                    var roleResult = userManager.AddToRole(user.Id, model.SelectedUserRole);

                    if (roleResult.Succeeded)
                    {
                        // Update the user
                        var userResult = await userManager.UpdateAsync(user);
                    }
                }
            }

            return RedirectToAction("_SystemUsersList");
        }


        [Authorize]
        public ActionResult _SystemUsersList()
        {
            var users = _unitOfWork.Account.GetAll();
            
            ViewModels.UsersListViewModel usersListViewModel = 
                new ViewModels.UsersListViewModel();
            
            List<ViewModels.UserViewModel> usersList = 
                new List<ViewModels.UserViewModel>();

            foreach (var user in users)
            {
                // Create a new view model for the user
                // and push the user database data to the viewmodel
                ViewModels.UserViewModel userViewModel =
                    new ViewModels.UserViewModel
                {
                        Id = user.UserId,
                        Email = user.Email,
                        PrivateName = "Or",
                        LastName = "Groman"
                };

                usersList.Add(userViewModel);
            }

            usersListViewModel.UsersEnumerable = usersList;

            return View(usersListViewModel);
        }

        // GET: UserDashboard
        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }
    }
}