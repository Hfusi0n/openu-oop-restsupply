using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Supplier;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class SupplierOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierOrderController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }
        // GET: SupplierOrder
        public ActionResult Index()
        {
            var dbSupplierOrders = _unitOfWork.SupplierOrders.GetAll();
            var dbSuppliers = _unitOfWork.Suppliers.GetAll();

            var supplierOrderIndexVm = new SupplierOrderIndexViewModel
            {
                CreateSupplierOrderViewModel = new CreateSupplierOrderViewModel
                {
                    AllSuppliers = dbSuppliers.Select(i => new SupplierViewModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Address = i.Address,
                        Phone = i.Phone
                    })
                },
                SupplierOrdersList = dbSupplierOrders.Select(s => new SupplierOrderViewModel
                {
                    SupplierId = s.SupplierId,
                    SupplierName = _unitOfWork.Suppliers.GetById(s.SupplierId).Name,
                    SupplierPhone = _unitOfWork.Suppliers.GetById(s.SupplierId).Phone,
                    SupplierAddress = _unitOfWork.Suppliers.GetById(s.SupplierId).Address,
                    OrderIngredientsList = s.IngredientListOrdersSet.Select(i => new SupplierOrderIngredientsViewModel
                    {
                        IngredientId = i.IngredientId,
                        IngredientName = _unitOfWork.Ingredients.GetById(s.SupplierId).Name,
                        Unit = _unitOfWork.Ingredients.GetById(s.SupplierId).Unit,
                        Amount = i.MoneyAmount,
                        OrderId = i.OrderId
                    })
                })
            };
            return View(supplierOrderIndexVm);
        }

        // GET: SupplierOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SupplierOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierOrder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplierOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
       
        // POST: SupplierOrder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplierOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierOrder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SaveSupplier(int supplierId, SupplierOrderIngredientsViewModel[] ingredients)
        {
            string result = "Error! Saving supplier process Is Not Complete!";
            if (supplierId < 1 && ingredients != null)
            {
                var supplierOrder = new IngredientOrders
                {
                    SupplierId = supplierId
                };
                foreach (var ingredientItem in ingredients)
                {
                    supplierOrder.IngredientListOrdersSet.Add(
                        new IngredientListOrders
                        {
                            IngredientId = ingredientItem.IngredientId,
                            MoneyAmount = ingredientItem.Amount
                        });
                }


                _unitOfWork.SupplierOrders.Add(supplierOrder);
                _unitOfWork.Complete();

                result = "Success! order is saved!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
