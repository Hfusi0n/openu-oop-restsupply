using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB;
using RestSupplyDB.Models.Menu;
using RestSupplyMVC.Persistence;
using RestSupplyMVC.ViewModels;

namespace RestSupplyMVC.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuItemsController()
        {
            _unitOfWork = new UnitOfWork(new RestSupplyDbContext());
        }

        // GET: MenuItems
        public ActionResult Index()
        {
            var dbMenuItems = _unitOfWork.MenuItems.GetAll();
            var dbIngredients = _unitOfWork.Ingredients.GetAll();

            var menuItemIndexVm = new MenuItemIndexViewModel();
            var menuItemsListVm = new List<MenuItemViewModel>();
            var createMenuItemVm = new MenuItemViewModel
            {
                AllIngredients = dbIngredients.Select(i => new IngredientViewModel
                {
                    IngredientId = i.Id,
                    Name = i.Name,
                    Unit = i.Unit
                })
            };
            menuItemIndexVm.MenuItemToCreate = createMenuItemVm;

            foreach (var dbMenuItem in dbMenuItems)
            {
                menuItemsListVm.Add(new MenuItemViewModel
                {
                    MenuItemId = dbMenuItem.Id,
                    Name = dbMenuItem.Name,
                    MenuItemIngredients = dbMenuItem.MenuIngredientsSet.Select(mi => new MenuItemIngredientViewModel
                    {
                        IngredientId = mi.IngredientId,
                        Name = _unitOfWork.Ingredients.GetById(mi.IngredientId).Name,
                        Quantity = mi.Quantity,
                        Unit = _unitOfWork.Ingredients.GetById(mi.IngredientId).Unit
                    })
                });
            }

            menuItemIndexVm.MenuItemViewModels = menuItemsListVm;

            return View(menuItemIndexVm);
        }

        // GET: MenuItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItems dbMenuItem = _unitOfWork.MenuItems.GetById(id.Value);
            var dbIngredients = _unitOfWork.Ingredients.GetAll();
            if (dbMenuItem == null)
            {
                return HttpNotFound();
            }

            var menuItemVm = new MenuItemViewModel
            {
                MenuItemId = dbMenuItem.Id,
                Name = dbMenuItem.Name,
                MenuItemIngredients = dbMenuItem.MenuIngredientsSet.Select(mi => new MenuItemIngredientViewModel
                {
                    IngredientId = mi.IngredientId,
                    Name = _unitOfWork.Ingredients.GetById(mi.IngredientId).Name,
                    Quantity = mi.Quantity,
                    Unit = _unitOfWork.Ingredients.GetById(mi.IngredientId).Unit
                }),
                AllIngredients = dbIngredients.Select(i => new IngredientViewModel
                {
                    IngredientId = i.Id,
                    Name = i.Name,
                    Unit = i.Unit
                })
            };

            return View(menuItemVm);
        }

        [HttpPost]
        public ActionResult AddMenuItemIngredients(int menuItemId,
            MenuItemIngredientViewModel[] ingredients)
        {
            string result = "Error! Unable to add ingredients!";
            if (menuItemId > 0 && ingredients.Any())
            {
                var menuItemIngredients = ingredients.Select(i => new MenuItemIngredients
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity
                }).Where(q => q.Quantity > 0).ToList();

                _unitOfWork.MenuItems.AddMenuItemIngredients(menuItemId,
                    menuItemIngredients);
                _unitOfWork.Complete();

                result = "Success! Ingredients were added!";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: MenuItems/Create
        public ActionResult Create()
        {
            var menuItemIngredientsVm = new List<MenuItemIngredientViewModel>();
            var menuItemVm = new MenuItemViewModel();

            var dbIngredients = _unitOfWork.Ingredients.GetAll();

            foreach (var ingredient in dbIngredients)
            {
                menuItemIngredientsVm.Add(new MenuItemIngredientViewModel
                {
                    Name = ingredient.Name,
                    IngredientId = ingredient.Id,
                    Unit = ingredient.Unit
                });
            }

            menuItemVm.MenuItemIngredients = menuItemIngredientsVm;

            return View(menuItemVm);
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItemViewModel vm)
        {
            

            if (ModelState.IsValid)
            {
                var menuItem = new MenuItems
                {
                    Name = vm.Name,
                    MenuIngredientsSet = (ICollection<MenuItemIngredients>) vm.MenuItemIngredients.Select(m =>
                        new MenuItemIngredients
                        {
                            IngredientId = m.IngredientId,
                            Quantity = m.Quantity,
                        })

                };
                _unitOfWork.MenuItems.Add(menuItem);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: MenuItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItems menuItems = _unitOfWork.MenuItems.GetById(id.Value);
            if (menuItems == null)
            {
                return HttpNotFound();
            }
            return View(menuItems);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] MenuItems model)
        {
            if (ModelState.IsValid)
            {
                var menuItem = _unitOfWork.MenuItems.GetById(model.Id);
                menuItem.Name = model.Name;

                // Update the unit of work 
                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: MenuItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItems menuItems = _unitOfWork.MenuItems.GetById(id.Value);
            if (menuItems == null)
            {
                return HttpNotFound();
            }
            return View(menuItems);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItems menuItems = _unitOfWork.MenuItems.GetById(id);
            _unitOfWork.MenuItems.Remove(menuItems);
            _unitOfWork.Complete();
            
            return RedirectToAction("Index");
        }
        public ActionResult SaveOrder(string menuItemName, MenuItemIngredientViewModel[] ingredients)
        {
            string result = "Error! Saving Menu Item Process Is Not Complete!";
            if (menuItemName != null && ingredients != null)
            {
                var menuItem = new MenuItems
                {
                    Name = menuItemName
                };
                foreach (var ingredientItem in ingredients)
                {
                    menuItem.MenuIngredientsSet.Add(
                        new MenuItemIngredients
                        {
                            IngredientId = ingredientItem.IngredientId,
                            Quantity = ingredientItem.Quantity
                        });

                }

                _unitOfWork.MenuItems.Add(menuItem);
                _unitOfWork.Complete();
                result = "Success! Menu Item is saved!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveIngredientFromMenuItem(int ingredientId, int menuItemId)
        {
            string result = "Error when removing ingredient from supplier!";
            if (ingredientId > 0 && menuItemId > 0)
            {
                _unitOfWork.MenuItems.RemoveMenuItemIngredient(menuItemId, ingredientId);
                _unitOfWork.Complete();
                result = "Success";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                /*db.Dispose();*/
            }
            base.Dispose(disposing);
        }


    }
}
