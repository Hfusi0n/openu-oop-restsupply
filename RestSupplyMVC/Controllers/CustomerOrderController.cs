using RestSupplyMVC.Persistence;
using System.Web.Mvc;

namespace RestSupplyMVC
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

        // GET: CustomerOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {

            var menuItemsList = _unitOfWork.MenuItems.GetAll();

            ViewModels.CustomerOrderViewModel customerOrder = new ViewModels.CustomerOrderViewModel
            {
                MenuItems = new SelectList(menuItemsList, "Value", "Text")
            };

            return View(customerOrder);
        }

        [HttpGet]
        public ActionResult AddOrder()
        {
            //ViewBag.SelectedMenuItem =

            //var employee = new Employee();

            return View();
        }

    }
}