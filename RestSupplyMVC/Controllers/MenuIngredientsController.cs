using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyDB.Models.Menu;
using RestSupplyMVC.Persistence;

namespace RestSupplyMVC.Controllers
{
    public class MenuIngredientsController : Controller
    {
        private RestSupplyDbContext db = new RestSupplyDbContext();
        private IUnitOfWork _unitOfWork;

        public MenuIngredientsController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }
        // GET: MenuIngredients
        public ActionResult Index()
        {
            var menuIngredientsSet = db.MenuIngredientsSet.Include(m => m.IngredientsSet).Include(m => m.MenuItemsSet);
            return View(menuIngredientsSet.ToList());
        }

        // GET: MenuIngredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItemIngredients menuIngredients = db.MenuIngredientsSet.Find(id);
            if (menuIngredients == null)
            {
                return HttpNotFound();
            }
            return View(menuIngredients);
        }

        // GET: MenuIngredients/Create
        public ActionResult Create()
        {
            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name");
            ViewBag.MenuItemId = new SelectList(db.MenuItemsSet, "Id", "Name");
            return View();
        }

        // POST: MenuIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Valid,IngredientName,IngredientId,MenuItemId,Quantity")] MenuItemIngredients menuIngredients)
        {
            if (ModelState.IsValid)
            {
                db.MenuIngredientsSet.Add(menuIngredients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name", menuIngredients.IngredientId);
            ViewBag.MenuItemId = new SelectList(db.MenuItemsSet, "Id", "Name", menuIngredients.MenuItemId);
            return View(menuIngredients);
        }

        // GET: MenuIngredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItemIngredients menuIngredients = db.MenuIngredientsSet.Find(id);
            if (menuIngredients == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name", menuIngredients.IngredientId);
            ViewBag.MenuItemId = new SelectList(db.MenuItemsSet, "Id", "Name", menuIngredients.MenuItemId);
            return View(menuIngredients);
        }

        // POST: MenuIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valid,IngredientName,IngredientId,MenuItemId,Quantity")] MenuItemIngredients menuIngredients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuIngredients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name", menuIngredients.IngredientId);
            ViewBag.MenuItemId = new SelectList(db.MenuItemsSet, "Id", "Name", menuIngredients.MenuItemId);
            return View(menuIngredients);
        }

        // GET: MenuIngredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItemIngredients menuIngredients = db.MenuIngredientsSet.Find(id);
            if (menuIngredients == null)
            {
                return HttpNotFound();
            }
            return View(menuIngredients);
        }

        // POST: MenuIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItemIngredients menuIngredients = db.MenuIngredientsSet.Find(id);
            db.MenuIngredientsSet.Remove(menuIngredients);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
