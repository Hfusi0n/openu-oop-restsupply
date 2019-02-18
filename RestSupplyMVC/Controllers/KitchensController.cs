using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyDB.Models.Kitchen;

namespace RestSupplyMVC.Controllers
{
    public class KitchensController : Controller
    {
        private RestSupplyDbContext db = new RestSupplyDbContext();

        // GET: Kitchens
        public ActionResult Index()
        {
            return View(db.KitchensSet.ToList());
        }

        // GET: Kitchens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchens kitchens = db.KitchensSet.Find(id);
            if (kitchens == null)
            {
                return HttpNotFound();
            }
            return View(kitchens);
        }

        // GET: Kitchens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kitchens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address")] Kitchens kitchens)
        {
            if (ModelState.IsValid)
            {
                db.KitchensSet.Add(kitchens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kitchens);
        }

        // GET: Kitchens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchens kitchens = db.KitchensSet.Find(id);
            if (kitchens == null)
            {
                return HttpNotFound();
            }
            return View(kitchens);
        }

        // POST: Kitchens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address")] Kitchens kitchens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitchens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kitchens);
        }

        // GET: Kitchens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchens kitchens = db.KitchensSet.Find(id);
            if (kitchens == null)
            {
                return HttpNotFound();
            }
            return View(kitchens);
        }

        // POST: Kitchens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitchens kitchens = db.KitchensSet.Find(id);
            db.KitchensSet.Remove(kitchens);
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
