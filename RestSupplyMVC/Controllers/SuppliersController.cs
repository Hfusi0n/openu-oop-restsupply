using System.Collections.Generic;
using RestSupplyDB;
using RestSupplyDB.Models.Supplier;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Menu;
using RestSupplyMVC.Helpers;

namespace RestSupplyMVC.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SuppliersController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }

        // GET: Suppliers
        [AuthorizeRoles(Role.BranchManager, Role.KitchenManager)]
        public ActionResult Index()
        {
            var dbIngredients = _unitOfWork.Ingredients.GetAll();
            var dbSuppliers = _unitOfWork.Suppliers.GetAll();

            var supplierIndexVm = new SupplierIndexViewModel
            {
                CreateSupplierViewModel = new CreateSupplierViewModel
                {
                    AllIngredients = dbIngredients.Select(i => new IngredientViewModel
                    {
                        IngredientId = i.Id,
                        Name = i.Name,
                        Unit = i.Unit
                    }).ToList()
                },
                SuppliersList = dbSuppliers.Select(s => new SupplierViewModel
                {
                    Id = s.Id,
                    Address = s.Address,
                    SupplierName = s.Name,
                    Phone = s.Phone,
                    AllSupplierIngredientsList = s.SuppliersIngredients.Select(i => new IngredientViewModel
                    {
                        IngredientId = i.Id
                    }).ToList()
                })
            };
            return View(supplierIndexVm);
        }

        // GET: Suppliers/Details/5
        [AuthorizeRoles(Role.BranchManager, Role.KitchenManager)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dbSupplier = _unitOfWork.Suppliers.GetById(id.Value);
            if (dbSupplier == null)
            {
                return HttpNotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            // Get all supplier details and orders
            var supplierDetailsVm = new SupplierOrderViewModel
            {
                Address = dbSupplier.Address,
                Id = dbSupplier.Id,
                SupplierName = dbSupplier.Name,
                Phone = dbSupplier.Phone,

                // All ingredients that supplier supplies
                AllSupplierIngredientsList = dbSupplier.SuppliersIngredients.Select(si => new IngredientViewModel
                {
                    IngredientId = si.IngredientId,
                    Name = _unitOfWork.Ingredients.GetById(si.IngredientId).Name,
                    Unit = _unitOfWork.Ingredients.GetById(si.IngredientId).Unit
                }).ToList(),
                AllIngredients = _unitOfWork.Ingredients.GetAll().Select(i => new IngredientViewModel
                {
                    IngredientId = i.Id,
                    Name = i.Name,
                    Unit = i.Unit
                }).ToList(),
                UserKitchens = _unitOfWork.Kitchens.GetKitchensByUserId(currentUserId).Select(k => new KitchenViewModel
                {
                    KitchenId = k.Id,
                    KitchenAddress = k.Address,
                    KitchenName = k.Name
                }).ToList()
            };


            return View(supplierDetailsVm);
        }

        // GET: Suppliers/Edit/5
        [AuthorizeRoles]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _unitOfWork.Suppliers.GetById(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            var supplierVm = new SupplierViewModel
            {
                Id = supplier.Id,
                Address = supplier.Address,
                SupplierName = supplier.Name,
                Phone = supplier.Phone
            };

            return View(supplierVm);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles]
        public ActionResult Edit(SupplierViewModel supplierVm)
        {
            if (ModelState.IsValid)
            {
                var dbSupplier = _unitOfWork.Suppliers.GetById(supplierVm.Id);

                dbSupplier.Address = supplierVm.Address;
                dbSupplier.Name = supplierVm.SupplierName;
                dbSupplier.Phone = supplierVm.Phone;
                
                _unitOfWork.Complete();

                return RedirectToAction("Details", new {id = dbSupplier.Id});
            }
            return View(supplierVm);
        }

        // GET: Suppliers/Delete/5
        [AuthorizeRoles]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = _unitOfWork.Suppliers.GetById(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            var supplierVm = new SupplierViewModel
            {
                Address = supplier.Address,
                Id = supplier.Id,
                SupplierName = supplier.Name,
                Phone = supplier.Phone,
                AllSupplierIngredientsList = supplier.SuppliersIngredients.Select(si => new IngredientViewModel
                {
                    IngredientId = si.IngredientId,
                    Name = _unitOfWork.Ingredients.GetById(si.IngredientId).Name,
                    Unit = _unitOfWork.Ingredients.GetById(si.IngredientId).Unit
                }).ToList()
            };
            
            return View(supplierVm);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles]
        public ActionResult DeleteConfirmed(int id)
        {
            var context = new RestSupplyDbContext();
            Supplier supplier = context.SuppliersSet.Find(id);
            if (supplier != null)
            {
                var result = context.SuppliersSet.Remove(supplier);
                context.SaveChanges();
                //_unitOfWork.Complete();
            }    
        
        return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult AddSupplierIngredients(int supplierId,
            IngredientViewModel[] ingredients)
        {
            string result = "Error! Unable to add ingredients!";
            if (supplierId > 0 && ingredients.Any())
            {
                _unitOfWork.Suppliers.AddSupplierIngredients(supplierId,
                    ingredients.Select(i => i.IngredientId).ToList());
                _unitOfWork.Complete();

                 result = "Success! Ingredients were added!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeRoles]
        public ActionResult RemoveIngredientFromSupplier(int ingredientId, int supplierId)
        {
            string result = "Error when removing ingredient from supplier!";
            if (ingredientId > 0 && supplierId > 0)
            {
                _unitOfWork.Suppliers.RemoveSupplierIngredient(supplierId, ingredientId);
                _unitOfWork.Complete();
                result = "Success";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeRoles]
        public ActionResult SaveSupplier(string supplierName, string supplierAddress, string supplierPhone,
            IngredientViewModel[] ingredients)
        {
            string result = "Error! Saving supplier process Is Not Complete!";
            if (supplierName != null && ingredients != null)
            {
                var supplier = new Supplier
                {
                    Name = supplierName,
                    Address = supplierAddress,
                    Phone = supplierPhone
                };
                foreach (var ingredientItem in ingredients)
                {
                    supplier.SuppliersIngredients.Add(
                        new SuppliersIngredients
                        {
                            IngredientId = ingredientItem.IngredientId
                        });
                }


                _unitOfWork.Suppliers.Add(supplier);
                _unitOfWork.Complete();

                result = "Success! Supplier is saved!";
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
