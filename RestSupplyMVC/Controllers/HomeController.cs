using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RestSupplyDB;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var kitchens = _unitOfWork.Kitchens.GetKitchensByUserId(currentUserId);

            var vm = new NavigationViewModel
            {
                UserKitchensList = kitchens.Select(k => new KitchenViewModel
                {
                    KitchenId = k.Id,
                    KitchenName = k.Name,
                    KitchenAddress = k.Address
                }).ToList(),
                ShowActions = User.Identity.IsAuthenticated,
            };
                
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}