using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;

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
            throw new NotImplementedException();
        }

        private Dictionary<int, double> GetOrderedIngredientsToQuantityMap(CustomerOrderDetailViewModel[] customerOrderVm)
        {
            var ingredientIdToQuantity = new Dictionary<int, double>();
            // Arrange all the ingredients and their amount in a dictionary
            // And for each ingredient - calculate the amount ordered
            foreach (var menuItem in customerOrderVm.Where(o => o.Quantity > 0).ToList())
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
            var dbMenuItems = _unitOfWork.MenuItems.GetAll().Where(mi => mi.MenuIngredientsSet.Any());
            var vm = new CreateCustomerOrderViewModel
            {
                AllMenuItemsToQuantityMap = dbMenuItems.ToDictionary(mi => new MenuItemViewModel
                {
                    MenuItemId = mi.Id,
                    Name = mi.Name
                }, q => 0),
                KitchenId = kitchenId,
                KitchenName = _unitOfWork.Kitchens.GetById(kitchenId).Name
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateCustomerOrderViewModel model)
        {            
            // TODO Create customerOrderRepository
            return View();
        }

        public ActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}