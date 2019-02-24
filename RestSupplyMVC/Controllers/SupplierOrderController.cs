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

        private OrdersViewModel GetOrdersViewModel()
        {
            var dbSupplierOrders = _unitOfWork.SupplierOrders.GetAll();

            var orderListViewModel = dbSupplierOrders.Select(s =>
            {
                // Get the relevant supplier details
                var supplier = _unitOfWork.Suppliers.GetById(s.SupplierId);
                double totalAmount = 0;

                var orderViewModel = new SupplierOrderViewModel
                {
                    Id = s.SupplierId,
                    SupplierName = supplier?.Name,
                    Phone = supplier?.Phone,
                    Address = supplier?.Address,
                    OrderId = s.Id,
                    OrderDate = s.Date,

                    // populate the all ingredient orders from the supplier
                    SupplierOrderIngredientsList = s.SupplierOrderDetails.Select(i =>
                    {
                        var ingredient = _unitOfWork.Ingredients.GetById(i.IngredientId);

                        var ingredientListViewModel = new SupplierOrderIngredientsViewModel
                        {
                            IngredientId = i.IngredientId,
                            Name = ingredient?.Name,
                            Unit = ingredient?.Unit,
                            Amount = i.Amount,
                            OrderId = i.OrderId
                        };

                        // Add to the order total price...
                        totalAmount += ingredientListViewModel.Amount;

                        return ingredientListViewModel;
                    }),
                    TotalAmount = totalAmount
                };

                // Update the order total price

                return orderViewModel;
            });


            var viewModel = new OrdersViewModel
            {
                Orders = orderListViewModel
            };
            

            return viewModel;
        }

        // GET: SupplierOrder
        [Authorize]
        public ActionResult Index(int? modalOrderId, string modalString)
        {
            OrdersViewModel viewModel = GetOrdersViewModel();

            viewModel = GetOrdersViewModel();

            if (!string.IsNullOrEmpty(modalString))
            {
                // Set the modal popup to the input order id
                viewModel.ModalOrderId = modalOrderId;

                switch (modalString)
                {
                    case "ShowEdit":
                        viewModel.DisplayEdit = true;
                        break;

                    case "ShowDetails":
                        viewModel.DisplayDetails = true;
                        break;

                    case "ShowDelete":        
                        viewModel.DisplayDelete = true;
                        break;
                }
            }

            return View(viewModel);
        }

        // GET: SupplierOrder/Details/5
        public ActionResult Details(int id)
        {
            /*
            var dbSupplierOrder = _unitOfWork.SupplierOrders.GetById(id);
            var supplierOrderVm = new SupplierOrderViewModel
            {
                OrderDate = dbSupplierOrder.Date,
                OrderId = dbSupplierOrder.Id,
                SupplierOrderIngredientsList = dbSupplierOrder.SupplierOrderDetails.Select(i =>
                    new SupplierOrderIngredientsViewModel
                    {
                        Amount = i.Amount,
                        IngredientId = i.IngredientId,
                        OrderId = i.OrderId,
                        Name = _unitOfWork.Ingredients.GetById(i.IngredientId).Name,
                        Unit = _unitOfWork.Ingredients.GetById(i.IngredientId).Unit,
                        OrderIngredientId = i.Id
                    }),
                Id = dbSupplierOrder.Id,
                Address = _unitOfWork.Suppliers.GetById(dbSupplierOrder.SupplierId).Address,
                SupplierName = _unitOfWork.Suppliers.GetById(dbSupplierOrder.SupplierId).Name,
                Phone = _unitOfWork.Suppliers.GetById(dbSupplierOrder.SupplierId).Phone

            };
            */
            return RedirectToAction("Index",
                new { modalOrderId = id, modalString ="ShowDetails" });
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

        public ActionResult SaveSupplierOrder(int supplierId, int kitchenId, SupplierOrderIngredientsViewModel[] ingredients)
        {
            string result = "Error! Saving supplier process Is Not Complete!";
            if (supplierId > 0 && ingredients != null && kitchenId > 0)
            {
                var supplierOrder = new SupplierOrders
                {
                    SupplierId = supplierId,
                    KitchenId = kitchenId,
                    Date = DateTime.Now
                };
                foreach (var ingredientItem in ingredients)
                {
                    supplierOrder.SupplierOrderDetails.Add(
                        new SupplierOrderDetails
                        {
                            IngredientId = ingredientItem.IngredientId,
                            Amount = ingredientItem.Amount
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
