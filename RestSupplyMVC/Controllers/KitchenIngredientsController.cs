﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        /// <summary>
        /// If user has more than one kitchen attached - redirected to Index with that kitchenId
        /// Else - redirected to a page where user can select the kitchen first. After kitchen is selected, user is redirected to Index action with the selected kitchenId
        /// </summary>
        public ActionResult Navigation()
        {
            var currentUserId = User.Identity.GetUserId();
            var kitchens = _unitOfWork.Kitchens.GetKitchensByUserId(currentUserId);
            if (kitchens.Count == 1)
            {
                var kitchenId = kitchens.First().Id;
                return RedirectToAction("Index", new { kitchenid = kitchenId });
            }

            return RedirectToAction("Index", "Kitchens", new { controllerRedirect = "KitchenIngredients" });
        }
        // id param = kitchenId
        // getting all ingredients for specific kitchen
        public ActionResult Index(int kitchenId)

        {
            // TODO security: check that KitchenId belong to current user
            // TODO GetIngredientIdToKitchenIngredientMap can return All ingredients in the keys
            // TODo Add Kitchen Name 
            var ingredientToKitchenIngredientMap =
               _unitOfWork.KitchenIngredient.GetIngredientIdToKitchenIngredientMap(kitchenId);
            var allIngredients = _unitOfWork.Ingredients.GetAll();
            var currentKitchen = _unitOfWork.Kitchens.GetById(kitchenId);
            var vm = new KitchenIngredientIndexViewModel
            {
                KitchenIngredientsList = allIngredients.Select(i => new KitchenIngredientViewModel
                {
                    IngredientId = i.Id,
                    Unit = i.Unit,
                    CurrentQuantity = ingredientToKitchenIngredientMap.ContainsKey(i.Id) ? ingredientToKitchenIngredientMap[i.Id].CurrentQuantity : (double?) null,
                    MinimalQuantity = ingredientToKitchenIngredientMap.ContainsKey(i.Id) ? ingredientToKitchenIngredientMap[i.Id].MinimalQuantity : (double?)null,
                    KitchenIngredientId = ingredientToKitchenIngredientMap.ContainsKey(i.Id) ? ingredientToKitchenIngredientMap[i.Id].Id : (int?)null,
                    IngredientName = i.Name,
                    KitchenId = kitchenId
                }).ToList(),
                KitchenId = kitchenId,
                KitchenName = currentKitchen.Name

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
        public ActionResult Create(int kitchenId, int ingredientId)
        {
            // Checking that KitchenIngredient doesn't already exist
            var ingredientToKitchenIngredientMap = _unitOfWork.KitchenIngredient.GetIngredientIdToKitchenIngredientMap(kitchenId);
            if (ingredientToKitchenIngredientMap.ContainsKey(ingredientId))
            {
                var kitchenIngredientId = ingredientToKitchenIngredientMap[ingredientId].Id;
                return RedirectToAction("Edit", new {id = kitchenIngredientId});
            }

            // Create new
            var kitchen = _unitOfWork.Kitchens.GetById(kitchenId);
            var ingredient = _unitOfWork.Ingredients.GetById(ingredientId);
            var vm = new KitchenIngredientViewModel
            {
                IngredientName = ingredient.Name,
                IngredientId = ingredient.Id,
                KitchenId = kitchen.Id,
                KitchenName = kitchen.Name,
                Unit = ingredient.Unit
            };
            return View(vm);
        }

        // POST: KitchenIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KitchenIngredientViewModel kitchenIngredientVm)
        {
            if (ModelState.IsValid)
            {
                KitchenIngredients kitchenIngredient = new KitchenIngredients
                {
                    CurrentQuantity = kitchenIngredientVm.CurrentQuantity ?? 0,
                    MinimalQuantity = kitchenIngredientVm.MinimalQuantity ?? 0,
                    IngredientId = kitchenIngredientVm.IngredientId,
                    KitchenId = kitchenIngredientVm.KitchenId
                };
                _unitOfWork.KitchenIngredient.Add(kitchenIngredient);
                _unitOfWork.Complete();

                return RedirectToAction("Index", new {kitchenId = kitchenIngredientVm.KitchenId});
            }

            return View(kitchenIngredientVm);
        }

        // GET: KitchenIngredients/Edit/5
        // @param id = KitchenIngredientId
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var kitchenIngredient = _unitOfWork.KitchenIngredient.GetById(id.Value);
            if (kitchenIngredient == null)
            {
                return HttpNotFound();
            }

            var vm = new KitchenIngredientViewModel
            {
                KitchenId = kitchenIngredient.KitchenId,
                CurrentQuantity = kitchenIngredient.CurrentQuantity,
                KitchenIngredientId = kitchenIngredient.Id,
                IngredientId = kitchenIngredient.IngredientId,
                IngredientName = kitchenIngredient.IngredientsSet.Name,
                Unit = kitchenIngredient.IngredientsSet.Unit,
                KitchenName = kitchenIngredient.KitchensSet.Name,
                MinimalQuantity = kitchenIngredient.MinimalQuantity
            };
            return View(vm);
        }

        // POST: KitchenIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KitchenIngredientViewModel kitchenIngredientVm)
        {
            if (!ModelState.IsValid || kitchenIngredientVm.KitchenIngredientId == null)
            {
                return View(kitchenIngredientVm);
                
            }

            // Updating Quantites
            var kitchenIngredient =
                _unitOfWork.KitchenIngredient.GetById(kitchenIngredientVm.KitchenIngredientId.Value);
            kitchenIngredient.CurrentQuantity = kitchenIngredientVm.CurrentQuantity ?? kitchenIngredient.CurrentQuantity;
            kitchenIngredient.MinimalQuantity = kitchenIngredientVm.MinimalQuantity ?? kitchenIngredient.MinimalQuantity;
            _unitOfWork.Complete();

            return RedirectToAction("Index", new { kitchenId = kitchenIngredientVm.KitchenId });

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
            var kitchenId = kitchenIngredients.KitchenId;
            return RedirectToAction("Index", new{kitchenId});
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
