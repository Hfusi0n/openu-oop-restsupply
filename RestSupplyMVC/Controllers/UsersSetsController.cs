using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyDB.Models;

namespace RestSupplyMVC.Controllers
{
    public class UsersSetsController : Controller
    {
        private RestSupplyDBModel db = new RestSupplyDBModel();

        // GET: UsersSets
        public async Task<ActionResult> Index()
        {
            return View(await db.GetUsers().ToListAsync());
        }

        // GET: UsersSets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersSet usersSet = db.GetUsers().Find(id);
            if (usersSet == null)
            {
                return HttpNotFound();
            }
            return View(usersSet);
        }

        // GET: UsersSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] UsersSet usersSet)
        {
            if (ModelState.IsValid)
            {
                db.GetUsers().Add(usersSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(usersSet);
        }

        // GET: UsersSets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersSet usersSet = db.GetUsers().Find(id);
            if (usersSet == null)
            {
                return HttpNotFound();
            }
            return View(usersSet);
        }

        // POST: UsersSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] UsersSet usersSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(usersSet);
        }

        // GET: UsersSets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersSet usersSet = db.GetUsers().Find(id);
            if (usersSet == null)
            {
                return HttpNotFound();
            }
            return View(usersSet);
        }

        // POST: UsersSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            UsersSet usersSet = db.GetUsers().Find(id);
            db.GetUsers().Remove(usersSet);
            await db.SaveChangesAsync();
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
