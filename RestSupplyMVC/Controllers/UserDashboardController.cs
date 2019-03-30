using Microsoft.AspNet.Identity;
using RestSupplyDB;
using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestSupplyMVC.Helpers;

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

        [AuthorizeRoles]
        public ActionResult Edit(string id)
        {
            // Get all roles from the database
            var user = _unitOfWork.Users.GetById(id);
            UserViewModel model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PrivateName = user.FirstName,
                LastName = user.LastName,
                UserRolesList = _unitOfWork.Roles.GetRolesByUserId(user.Id).Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    RoleName = _unitOfWork.Roles.GetById(r.RoleId).Name
                }).ToList(),
                AllRolesList = _unitOfWork.Roles.GetAll().Select(r => new RoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                }).ToList()
            };



            return View(model);
        }

        // POST: Update the user data
        [HttpPost]
        [AuthorizeRoles]
        public ActionResult Edit(UserViewModel vm)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(vm.Id))
            {
                return RedirectToAction("Index");
            }
            var userToUpdate = _unitOfWork.Users.GetById(vm.Id);
            userToUpdate.FirstName = vm.PrivateName;
            userToUpdate.LastName = vm.LastName;

            _unitOfWork.Complete();

            //// Create a user manager
            AppUserManager userManager = new AppUserManager(new AppUserStore(_dbContext));

            foreach (var roleName in vm.UpdatedUserRoleNamesArr)
            {
                userManager.AddToRole(userToUpdate.Id, roleName);
            }

            return RedirectToAction("Index");
        }


        [AuthorizeRoles]
        public ActionResult Index()
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
                }).ToList();
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
        [AuthorizeRoles]
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
                    var dbRoles = _unitOfWork.Roles.GetAll();
                    var userRoles = user.Roles.ToList();

                    List<RoleViewModel> userRolesViewModel =
                        userRoles.Select(x => new RoleViewModel()
                        {
                            RoleId = x.RoleId,
                            RoleName = dbRoles.FirstOrDefault(s => s.Id == x.RoleId).Name
                        }).ToList();

                    List<RoleViewModel> allRolesViewModel = dbRoles.Select(x => new RoleViewModel()
                    {
                        RoleId = x.Id,
                        RoleName = x.Name
                    }).ToList();



                    var userVm = new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        PrivateName = user.FirstName,
                        LastName = user.LastName,
                        AllRolesList = allRolesViewModel,
                        UserRolesList = userRolesViewModel
                    };
                    return View("Edit", userVm);
                }
            }


            return RedirectToAction("Admin");
        }

        // GET: UserDashboard
        [Authorize(Roles = "Admin")]
        [AuthorizeRoles]
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