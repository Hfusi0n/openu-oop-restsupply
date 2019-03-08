using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RestSupplyDB;
using RestSupplyMVC.Persistence;

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

            return View();
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