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
using RestSupplyMVC.Helpers;
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
        [AuthorizeRoles]
        public ActionResult Index()
        {
            var dbIngredients = _unitOfWork.Ingredients.GetAll();
            var dbSuppliers = _unitOfWork.Suppliers.GetAll();

            var ingredientIndexVm = new IngredientIndexViewModel
            {
                IngredientToCreate = new CreateIngredientViewModel
                {
                    AllSuppliers = dbSuppliers.Select(i => new SupplierViewModel
                    {
                        Id = i.Id,
                        SupplierName = i.Name,
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
        [AuthorizeRoles]
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
                    SupplierName = si.Supplier.Name,
                    Address = si.Supplier.Address,
                    Phone = si.Supplier.Phone
                })
            };
            return View(ingredientVm);
        }

        // GET: Ingredients/Edit/5
        [AuthorizeRoles]
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
            return View(ingredientVm);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles]
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
        [AuthorizeRoles]
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
        [AuthorizeRoles]
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

        [AuthorizeRoles]
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

                if (suppliers != null && suppliers.Any())
                {
                    foreach (var supplier in suppliers)
                    {
                        ingredient.SuppliersIngredients.Add(
                            new SuppliersIngredients
                            {
                                SupplierId = supplier.Id,
                            });
                    }
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
