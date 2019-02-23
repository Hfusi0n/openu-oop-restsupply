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
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class KitchenIngredientsController : Controller
    {
        private RestSupplyDbContext db = new RestSupplyDbContext();
        private readonly IUnitOfWork _unitOfWork;

        public KitchenIngredientsController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }
        

        // id param = kitchenId
        // getting all ingredients for specific kitchen
        public ActionResult Index(int? id)

        {
            if (id == null)
            {
                // TODO 
                return RedirectToAction("index", "Kitchens");
            }

            // TODO security: check that KitchenId belong to current user

            var ingredientToKitchenIngredientMap =
                _unitOfWork.KitchenIngredient.GetIngredientIdToKitchenIngredientMap(id.Value);
            var allIngredients = _unitOfWork.Ingredients.GetAll();
            var vm = new KitchenIngredientIndexViewModel
            {
                KitchenId = id.Value,
                KitchenIngredientsList = allIngredients.Select(i => new KitchenIngredientViewModel
                {
                    IngredientId = i.Id,
                    Unit = i.Unit,
                    CurrentQuantity = ingredientToKitchenIngredientMap.ContainsKey(i.Id) ? ingredientToKitchenIngredientMap[i.Id].CurrentQuantity : (double?) null,
                    MinimalQuantity = ingredientToKitchenIngredientMap.ContainsKey(i.Id) ? ingredientToKitchenIngredientMap[i.Id].MinimalQuantity : (double?)null,
                    KitchenIngredientId = ingredientToKitchenIngredientMap.ContainsKey(i.Id) ? ingredientToKitchenIngredientMap[i.Id].Id : (int?)null

                }).ToList()
            };
            return View(vm);
        }



        // GET: KitchenIngredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitchenIngredients kitchenIngredients = db.KitchenIngredientsSet.Find(id);
            if (kitchenIngredients == null)
            {
                return HttpNotFound();
            }
            return View(kitchenIngredients);
        }

        // GET: KitchenIngredients/Create
        public ActionResult Create()
        {
            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name");
            ViewBag.KitchenId = new SelectList(db.KitchensSet, "Id", "Name");
            return View();
        }

        // POST: KitchenIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Valid,KitchenId,IngredientId,MinimalQuantity,CurrentQuantity")] KitchenIngredients kitchenIngredients)
        {
            if (ModelState.IsValid)
            {
                db.KitchenIngredientsSet.Add(kitchenIngredients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name", kitchenIngredients.IngredientId);
            ViewBag.KitchenId = new SelectList(db.KitchensSet, "Id", "Name", kitchenIngredients.KitchenId);
            return View(kitchenIngredients);
        }

        // GET: KitchenIngredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitchenIngredients kitchenIngredients = db.KitchenIngredientsSet.Find(id);
            if (kitchenIngredients == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name", kitchenIngredients.IngredientId);
            ViewBag.KitchenId = new SelectList(db.KitchensSet, "Id", "Name", kitchenIngredients.KitchenId);
            return View(kitchenIngredients);
        }

        // POST: KitchenIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valid,KitchenId,IngredientId,MinimalQuantity,CurrentQuantity")] KitchenIngredients kitchenIngredients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitchenIngredients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngredientId = new SelectList(db.IngredientsSet, "Id", "Name", kitchenIngredients.IngredientId);
            ViewBag.KitchenId = new SelectList(db.KitchensSet, "Id", "Name", kitchenIngredients.KitchenId);
            return View(kitchenIngredients);
        }

        // GET: KitchenIngredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitchenIngredients kitchenIngredients = db.KitchenIngredientsSet.Find(id);
            if (kitchenIngredients == null)
            {
                return HttpNotFound();
            }
            return View(kitchenIngredients);
        }

        // POST: KitchenIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KitchenIngredients kitchenIngredients = db.KitchenIngredientsSet.Find(id);
            db.KitchenIngredientsSet.Remove(kitchenIngredients);
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
