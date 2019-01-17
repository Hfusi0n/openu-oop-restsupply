using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;
using DBAppUser = RestSupplyDB.Models.AppUser;

namespace RestSupplyMVC.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly RestSupplyDB.RestSupplyDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public CustomerOrderController()
        {
            _context = new RestSupplyDB.RestSupplyDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        [Authorize]
        public ActionResult Create()
        {           
            var menuItemsList = _unitOfWork.MenuItems.GetAll();

            CustomerOrderViewModel customerOrderViewModel = new CustomerOrderViewModel
            {
                MenuItems = new SelectList(menuItemsList, "Id", "Name")
            };

            return View(customerOrderViewModel);            
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CustomerOrderViewModel model,
            string submitButton)
        {
            switch (submitButton)
            {
                case "Value1":
                    break;
                case "Value2":
                    break;
            }

            return View(model);
        }

    }
}