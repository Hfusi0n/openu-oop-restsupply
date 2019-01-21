using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerOrderController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
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