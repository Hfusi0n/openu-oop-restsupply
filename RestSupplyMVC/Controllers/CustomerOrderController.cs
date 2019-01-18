using System;
using System.Collections.Generic;
using System.Web.Mvc;
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
            CustomerOrderViewModel model = new CustomerOrderViewModel()
            {
                MenuItems = new SelectList(_unitOfWork.MenuItems.GetAll(), "Id", "Name", 1),
                Orders = new Dictionary<string, string>()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(List<string> Orders)
        {            
            return View();
        }

    }
}