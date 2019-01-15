using System.Web.Mvc;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.Repositories;
using RestSupplyMVC.ViewModels;

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

        public ActionResult Create()
        {
            var menuItemsList = _unitOfWork.MenuItems.GetAll();

            CustomerOrderViewModel customerOrderViewModel = new CustomerOrderViewModel
            {
                MenuItems = new SelectList(menuItemsList, "Id", "Name")
            };

            return View(customerOrderViewModel);            
        }

    }
}