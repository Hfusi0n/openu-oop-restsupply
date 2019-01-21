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
        public string Name { get; set; }
        public List<Ingredients> IngredientList { get; set; }
        public List<Ingredients> SelectedIngredientList { get; set; }
    }
}