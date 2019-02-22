using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Kitchen;
using RestSupplyDB.Models.Supplier;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class KitchensController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private RestSupplyDbContext db = new RestSupplyDbContext();

        public KitchensController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }
        // GET: Kitchens
        public ActionResult Index()
        {
            var dbUsers = _unitOfWork.Account.GetAll();
            var dbKitchens = _unitOfWork.Kitchens.GetAll();
            var dbRoles = _unitOfWork.Account.GetAppRoles();
            var vm = new KitchenIndexViewModel
            {
                KitchenToCreate = new CreateKitchenViewModel
                {
                    AllUsersList = dbUsers.Select(
                        u => new UserViewModel
                        {
                            Id = u.UserId,
                            Email = u.Email,
                            LastName = u.LastName,
                            PrivateName = u.FirstName,
                            SelectedUserRole = (u.Role != null)
                                ? dbRoles.FirstOrDefault(r => r.Id == u.Role.RoleId)?.Name
                                : null

                        }).ToList()

                },
                KitchensList = dbKitchens.Select(k => new KitchenViewModel
                {
                    KitchenName = k.Name,
                    KitchenAddress = k.Address,
                    KitchenId = k.Id,
                    
                }).ToList()
            };
            return View(vm);
        }

        // GET: Kitchens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var allUsers = _unitOfWork.Account.GetAll();
            var kitchen = _unitOfWork.Kitchens.GetById(id.Value);

            // TODO add user role
            var dbRoles = _unitOfWork.Account.GetAppRoles();
            var kitchenVm = new CreateKitchenViewModel
            {
                KitchenUsersList = kitchen.KitchenUsers.Select(u => new UserViewModel
                {
                    Id = u.UserId,
                    Email = _unitOfWork.Account.GetById(u.UserId).Email
                }).ToList(),
                AllUsersList = allUsers.Select(us => new UserViewModel
                {
                    Id = us.UserId,
                    Email = _unitOfWork.Account.GetById(us.UserId).Email
                }).ToList(),
                KitchenName = kitchen.Name,
                KitchenAddress = kitchen.Address,
                KitchenId = kitchen.Id

            };

            return View(kitchenVm);
        }

        public ActionResult SaveKitchen(string kitchenName, string kitchenAddress, UserViewModel[] users)
        {
            string result = "Error! Saving Kitchen Process Is Not Complete!";
            if (kitchenName != null)
            {
                var kitchen = new Kitchens()
                {
                    Name = kitchenName,
                    Address = kitchenAddress
                };
                foreach (var user in users)
                {
                    kitchen.KitchenUsers.Add(
                        new KitchenUsers
                        {
                            UserId = user.Id,
                        });
                }

                _unitOfWork.Kitchens.Add(kitchen);
                _unitOfWork.Complete();
                result = "Success! Kitchen is saved!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddUsersToKitchen(int kitchenId, UserViewModel[] users)
        {
            string result = "Error! Unable to add ingredients!";
            if (kitchenId > 0 && users.Any())
            {
                var userIds = users.Select(u => u.Id).ToList();
                _unitOfWork.Kitchens.AddUsersToKitchen(kitchenId, userIds);
                _unitOfWork.Complete();

                result = "Success! Users were added!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
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
