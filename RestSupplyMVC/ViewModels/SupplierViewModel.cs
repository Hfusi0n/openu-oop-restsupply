using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.ViewModels
{
    public class SupplierViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Ingredient { get; set; }
        public IEnumerable<Ingredients> IngredientList { get; set; }
    }
}