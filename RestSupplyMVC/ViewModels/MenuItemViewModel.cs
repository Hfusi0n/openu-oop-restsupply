using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.ViewModels
{
    public class MenuItemViewModel
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public IEnumerable<MenuItemIngredientViewModel> MenuItemIngredients { get; set; }
        public List<IngredientViewModel> NotAssociatedIngredients
        {
            get { if (AllIngredients.Any() && MenuItemIngredients.Any()) return AllIngredients.Where(i => MenuItemIngredients.All(si => si.IngredientId != i.IngredientId)).ToList();
            return  new List<IngredientViewModel>();}

        }
        public IEnumerable<IngredientViewModel> AllIngredients { get; set; }

    }

    public class MenuItemIndexViewModel
    {
        public IEnumerable<MenuItemViewModel> MenuItemViewModels { get; set; }
        public MenuItemViewModel MenuItemToCreate { get; set; }
    }

}