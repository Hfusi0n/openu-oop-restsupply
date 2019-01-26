using System;
using System.Collections.Generic;
using System.Linq;
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
            var dbMenuItems = _unitOfWork.MenuItems.GetAll();
            var model = new CreateCustomerOrderViewModel
            {
                AllMenuItemsToQuantityMap = dbMenuItems.ToDictionary(mi => new MenuItemViewModel
                {
                    Id = mi.Id,
                    Name = mi.Name
                }, q => 0)
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateCustomerOrderViewModel model)
        {            
            // TODO Create customerOrderRepository
            return View();
        }

    }
}