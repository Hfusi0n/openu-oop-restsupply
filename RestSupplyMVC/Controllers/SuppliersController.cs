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
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Menu;

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
                    })
                },
                SuppliersList = dbSuppliers.Select(s => new SupplierViewModel
                {
                    Id = s.Id,
                    Address = s.Address,
                    Name = s.Name,
                    Phone = s.Phone,
                    AllSupplierIngredientsList = s.SuppliersIngredients.Select(i => new IngredientViewModel
                    {
                        IngredientId = i.Id
                    })
                })
            };
            return View(supplierIndexVm);
        }

        // GET: Suppliers/Details/5
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

            // Get all supplier details and orders
            var supplierDetailsVm = new SupplierOrderViewModel
            {
                Address = dbSupplier.Address,
                Id = dbSupplier.Id,
                Name = dbSupplier.Name,
                Phone = dbSupplier.Phone,

                // All ingredients that supplier supplies
                AllSupplierIngredientsList = dbSupplier.SuppliersIngredients.Select(si => new IngredientViewModel
                {
                    IngredientId = si.IngredientId,
                    Name = _unitOfWork.Ingredients.GetById(si.IngredientId).Name,
                    Unit = _unitOfWork.Ingredients.GetById(si.IngredientId).Unit
                })                
            };


            return View(supplierDetailsVm);
        }

        //[Authorize]
        public ActionResult Create()
        {
            var viewModel = new CreateSupplierViewModel
            {
                AllIngredients = _unitOfWork.Ingredients.GetAll().Select(i => new IngredientViewModel
                {
                    IngredientId = i.Id,
                    Name = i.Name,
                    Unit = i.Unit
                })
            };
            return View(viewModel);
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    //IngredientsSet = new List<Ingredients>() TODO
                    Phone = viewModel.Phone
                };

                _unitOfWork.Suppliers.Add(supplier);
                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Suppliers/Edit/5
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
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                new RestSupplyDbContext().Entry(supplier).State = EntityState.Modified;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
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

            SupplierViewModel supplierVm = new SupplierViewModel
            {
                Address = supplier.Address,
                Id = supplier.Id,
                Name = supplier.Name,
                Phone = supplier.Phone,
                AllSupplierIngredientsList = supplier.SuppliersIngredients.Select(si => new IngredientViewModel
                {
                    IngredientId = si.IngredientId,
                    Name = _unitOfWork.Ingredients.GetById(si.IngredientId).Name,
                    Unit = _unitOfWork.Ingredients.GetById(si.IngredientId).Unit
                })
            };
            
            return View(supplierVm);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var context = new RestSupplyDbContext();
            Supplier supplier = context.SuppliersSet.Find(id);
            if (supplier != null)
            {
                context.SuppliersSet.Remove(supplier);
                _unitOfWork.Complete();
            }    
        
        return RedirectToAction("Index");
        }

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
