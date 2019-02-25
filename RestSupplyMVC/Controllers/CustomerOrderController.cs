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

        [HttpPost]
        public ActionResult IsMenuOrderInStock(CustomerOrderDetailViewModel[] orderVm, int kitchenId)
        {
            // here we also need to pass the KitchenId in order to get a result. 
            // Get kitchenId from currentUser?


            // step 1: foreach ingredient - make sure amount in current kitchen is listed. if not - not in stock
            // step 2: foreach ingredient - calculate the amount ordered

            return Json(true);
        }
        [Authorize]
        public ActionResult Create(int kitchenId)
        {
            var dbMenuItems = _unitOfWork.MenuItems.GetAll();
            var vm = new CreateCustomerOrderViewModel
            {
                AllMenuItemsToQuantityMap = dbMenuItems.ToDictionary(mi => new MenuItemViewModel
                {
                    Id = mi.Id,
                    Name = mi.Name
                }, q => 0),
                KitchenId = kitchenId,
                KitchenName = _unitOfWork.Kitchens.GetById(kitchenId).Name
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateCustomerOrderViewModel model)
        {            
            // TODO Create customerOrderRepository
            return View();
        }

        public ActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}