using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.ViewModels
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MenuItemIngredientViewModel> MenuItemIngredients { get; set; }
    }

    public class MenuItemIndexViewModel
    {
        public IEnumerable<MenuItemViewModel> MenuItemViewModels { get; set; }
        public CreateMenuItemViewModel CreateMenuItemViewModel { get; set; }
    }

    public class CreateMenuItemViewModel : MenuItemViewModel
    {
        public IEnumerable<IngredientViewModel> AllIngredients { get; set; }
    }
}