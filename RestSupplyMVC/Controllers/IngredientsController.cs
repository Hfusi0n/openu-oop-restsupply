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
using RestSupplyDB.Models.Supplier;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.Repositories;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientsController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }

        // GET: Ingredients
        public ActionResult Index()
        {
            var dbIngredients = _unitOfWork.Ingredients.GetAll();
            var dbSuppliers = _unitOfWork.Suppliers.GetAll();

            var ingredientIndexVm = new IngredientIndexViewModel
            {
                CreateIngredientViewModel = new CreateIngredientViewModel
                {
                    AllSuppliers = dbSuppliers.Select(i => new SupplierViewModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Address = i.Address,
                        Phone = i.Phone
                    })
                },
                IngredientsList = dbIngredients.Select(s => new IngredientViewModel
                {
                    IngredientId = s.Id,
                    Name = s.Name,
                    Unit = s.Unit,
                    
                    AllIngredientSuppliersList = s.SuppliersIngredients.Select(i => new SupplierViewModel
                    {
                        Id = i.Id
                    })
                })
            };
            return View(ingredientIndexVm);
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ingredients ingredients = _unitOfWork.Ingredients.GetById(id.Value);
            if (ingredients == null)
            {
                return HttpNotFound();
            }

            var ingredientVm = new IngredientViewModel
            {
                IngredientId = ingredients.Id,
                Name = ingredients.Name,
                Unit = ingredients.Unit,
                AllIngredientSuppliersList = ingredients.SuppliersIngredients.Select(si => new SupplierViewModel
                {
                    Id = si.Id,
                    Name = _unitOfWork.Suppliers.GetById(si.IngredientId).Name,
                    Address = _unitOfWork.Suppliers.GetById(si.IngredientId).Address,
                    Phone = _unitOfWork.Suppliers.GetById(si.IngredientId).Phone
                })
            };
            return View(ingredientVm);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var createdIngredient = new Ingredients
                {
                    Name = vm.Name,
                    Unit = vm.Unit
                };
                _unitOfWork.Ingredients.Add(createdIngredient);
                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients dbIngredient = _unitOfWork.Ingredients.GetById(id.Value);

            var ingredientVm = new IngredientViewModel
            {
                IngredientId = dbIngredient.Id,
                Name = dbIngredient.Name,
                Unit = dbIngredient.Unit
            };
            if (dbIngredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredientVm);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredientId,Name,Unit")] IngredientViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var ingredient = _unitOfWork.Ingredients.GetById(vm.IngredientId);

            ingredient.Name = vm.Name;
            ingredient.Unit = vm.Unit;
            _unitOfWork.Complete();

            return View(vm);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = _unitOfWork.Ingredients.GetById(id.Value);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredients ingredients = _unitOfWork.Ingredients.GetById(id);
            _unitOfWork.Ingredients.Remove(ingredients);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult GetUnitByIngredientId(int id)
        {
            var response = _unitOfWork.Ingredients.GetById(id).Unit;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveIngredient(string ingredientName, string unit, SupplierViewModel[] suppliers)
        {
            string result = "Error! Saving ingredient Process Is Not Complete!";
            if (ingredientName != null && unit != null)
            {
                var ingredient = new Ingredients
                {
                    Name = ingredientName,
                    Unit = unit
                };
                foreach (var supplier in suppliers)
                {
                    ingredient.SuppliersIngredients.Add(
                        new SuppliersIngredients
                        {
                            SupplierId = supplier.Id,
                        });
                }

                _unitOfWork.Ingredients.Add(ingredient);
                _unitOfWork.Complete();
                result = "Success! Ingredient is saved!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                new RestSupplyDbContext().Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
