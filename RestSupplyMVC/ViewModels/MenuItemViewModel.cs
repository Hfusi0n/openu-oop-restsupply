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
        public IEnumerable<MenuItemIngredientViewModel> MenuItemIngredients{ get; set; }
        public string Heading { get; set; }
        public string Action => (Id != 0) ? "Update" : "Create";
        public IEnumerable<MenuItemIngredientViewModel> SelectedMenuItemIngredients { get; set; }
    }

    public class MenuItemIngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }

    }
}