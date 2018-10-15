using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;

namespace RestSupplyMVC.Controllers
{
    public class MenuItemsController : Controller
    {
        // GET: MenuItems
        public ActionResult Index()
        {
            using (RestSupplyDBModelContainer db = new RestSupplyDBModelContainer())
            {

            }
                return View();
        }

        // GET: MenuItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuItems/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: MenuItems/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuItems/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuItems/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuItems/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
