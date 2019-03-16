using RestSupplyDB;
using RestSupplyDB.Models.Customer;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestSupplyMVC.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerOrderController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }

        [HttpPost]
        public ActionResult IsMenuOrderInStock(CustomerOrderDetailViewModel[] orderVm, int kitchenId)
        {
            var ingredientsNotInKitchen = new List<IngredientViewModel>();
            var ingredientsNotInStock = new List<IngredientViewModel>();

            var ingredientIdToQuantity = GetOrderedIngredientsToQuantityMap(orderVm);
            // foreach ingredient - make sure amount in current kitchen is listed. if not - not in stock
            foreach (var orderedIngredientId in ingredientIdToQuantity.Keys)
            {
                var orderedQuantity = ingredientIdToQuantity[orderedIngredientId];

                var kitchenIngredient =
                    _unitOfWork.KitchenIngredient.GetByKitchenAndIngredientIds(kitchenId, orderedIngredientId);

                // if ingredient is not listed in the current kitchen
                if (kitchenIngredient == null)
                {
                    ingredientsNotInKitchen.Add(new IngredientViewModel
                    {
                        IngredientId = orderedIngredientId,
                        Name = _unitOfWork.Ingredients.GetById(orderedIngredientId)?.Name
                    });
                    continue;
                }

                // check if the ordered amount is not in stock
                var kitchenIngredientCurrentQuantity = kitchenIngredient.CurrentQuantity;
                if (orderedQuantity > kitchenIngredientCurrentQuantity)
                    ingredientsNotInStock.Add(new IngredientViewModel
                    {
                        IngredientId = orderedIngredientId,
                        Name = _unitOfWork.Ingredients.GetById(orderedIngredientId).Name
                    });
            }

            // Order is in stock iff all ingredients are in stock & listed in the kitchen
            bool isInStock = !ingredientsNotInStock.Any() && !ingredientsNotInKitchen.Any();

            var response = new
            {
                isInStock,
                ingredientsNotInStock,
                ingredientsNotInKitchen
            };


            return Json(response);
        }

        [HttpPost]
        public ActionResult CreateCustomerOrder(CustomerOrderDetailViewModel[] orderVm, int kitchenId)
        {
            string response = "Error! Saving Order Process Is Not Complete!";

            if (kitchenId > 0 && orderVm != null)
            {
                var orderedMenuItems = orderVm.Where(o => o.Quantity > 0).ToList();
                _unitOfWork.CustomerOrder.Add(new CustomerOrders
                {
                    Date = DateTime.Now,
                    CustomerDetailOrders = orderedMenuItems.Select(mi => new CustomerDetailOrders
                    {
                        MenuItemId = mi.MenuItemId,
                        Quantity = mi.Quantity
                    }).ToList(),
                    KitchenId = kitchenId
                });

                foreach (var orderedMenuItem in orderedMenuItems)
                {
                    var orderedMenuItemIngredientList = _unitOfWork.MenuItems.GetById(orderedMenuItem.MenuItemId).MenuIngredientsSet.ToList();
                    foreach (var orderedMenuItemIngredient in orderedMenuItemIngredientList)
                    {
                        // calculate amount of each ingredient in the order and reduce the amount from current kitchen
                        var kitchenIngredient =
                            _unitOfWork.KitchenIngredient.GetByKitchenAndIngredientIds(kitchenId,
                                orderedMenuItemIngredient.IngredientId);
                        if (kitchenIngredient != null)
                        {
                            kitchenIngredient.CurrentQuantity -= orderedMenuItem.Quantity * orderedMenuItemIngredient.Quantity;
                        }
                        else
                        {
                            response = "Error! Unable to update ingredient quantity for selected kitchen!";
                            return Json(response, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                _unitOfWork.Complete();
                response = "Success! Order is saved!";
            }

            return Json(response);
        }
    

    private Dictionary<int, double> GetOrderedIngredientsToQuantityMap(CustomerOrderDetailViewModel[] customerOrderVm)
        {
            var ingredientIdToQuantity = new Dictionary<int, double>();
            // Arrange all the ingredients and their amount in a dictionary
            // And for each ingredient - calculate the amount ordered
            var menuItemsWithQuantity = customerOrderVm.Where(o => o.Quantity > 0).ToList();
            foreach (var menuItem in menuItemsWithQuantity)
            {
                var orderIngredients = _unitOfWork.MenuItems.GetById(menuItem.MenuItemId).MenuIngredientsSet.ToList();
                foreach (var menuItemIngredient in orderIngredients)
                {
                    var totalIngredientQuantityInOrder = menuItemIngredient.Quantity * menuItem.Quantity;
                    if (ingredientIdToQuantity.ContainsKey(menuItemIngredient.IngredientId))
                    {
                        ingredientIdToQuantity[menuItemIngredient.IngredientId] += totalIngredientQuantityInOrder;
                    }
                    else
                    {
                        ingredientIdToQuantity.Add(menuItemIngredient.IngredientId, totalIngredientQuantityInOrder);
                    }
                }
            }

            return ingredientIdToQuantity;
        }

        [Authorize]
        public ActionResult Create(int kitchenId)
        {
            var dbMenuItems = _unitOfWork.MenuItems.GetAll().Where(mi => mi.MenuIngredientsSet.Any()).ToList();
            var vm = new CreateCustomerOrderViewModel
            {
                AllMenuItemsToQuantityMap = dbMenuItems.ToDictionary(mi => new MenuItemViewModel
                {
                    MenuItemId = mi.Id,
                    Name = mi.Name,
                    AllIngredients = _unitOfWork.Ingredients.GetAll().Select(i => new IngredientViewModel
                    {
                        IngredientId = i.Id,
                        Name = i.Name,
                        Unit = i.Unit,
                    }).ToList(),
                    MenuItemIngredients = _unitOfWork.MenuItems.GetById(mi.Id).MenuIngredientsSet.Select(i => new MenuItemIngredientViewModel
                    {
                        IngredientId = i.IngredientId
                    }).ToList()
                }, q => 0),
                KitchenId = kitchenId,
                KitchenName = _unitOfWork.Kitchens.GetById(kitchenId).Name
                
            };

            return View(vm);
        }

        public ActionResult Index(int kitchenId)
        {
            var customerOrderList = _unitOfWork.CustomerOrder.GetAllByKitchenId(kitchenId);
            var vm = customerOrderList.Select(c => new CustomerOrderViewModel
            {
                KitchenId = c.KitchenId,
                KitchenName = _unitOfWork.Kitchens.GetById(c.KitchenId).Name,
                Date = c.Date,
                CustomerOrderId = c.Id
                
            }).ToList();
            return View(vm);
        }

        public ActionResult Details(int customerOrderId)
        {
            var customerOrder = _unitOfWork.CustomerOrder.GetById(customerOrderId);
            if (customerOrder != null)
            {
                var vm = new CustomerOrderViewModel
                {
                    KitchenId = customerOrder.KitchenId,
                    KitchenName = _unitOfWork.Kitchens.GetById(customerOrder.KitchenId).Name,
                    Date = customerOrder.Date,
                    CustomerOrderDetailsList = customerOrder.CustomerDetailOrders.Select(d => new CustomerOrderDetailViewModel
                    {
                        CustomerOrderDetailId = d.Id,
                        MenuItemId = d.MenuItemId,
                        Name = _unitOfWork.MenuItems.GetById(d.MenuItemId).Name,
                        Quantity = d.Quantity,
                        AllIngredients = _unitOfWork.Ingredients.GetAll().Select(i => new IngredientViewModel
                        {
                            IngredientId = i.Id
                        }),
                        MenuItemIngredients = _unitOfWork.MenuItems.GetById(d.MenuItemId).MenuIngredientsSet.Select(mii => new MenuItemIngredientViewModel
                        {
                            IngredientId = mii.IngredientId
                        }).ToList()
                    })
                };
                return View(vm);
            }

            return RedirectToAction("Index");
        }
    }
}