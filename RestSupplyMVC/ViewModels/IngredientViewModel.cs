using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.ViewModels
{
    public class IngredientViewModel
    {
        public int IngredientId { get; set; }

        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        [Display(Name = "Ingredient Unit")]
        public string Unit { get; set; }

        public IEnumerable<SupplierViewModel> AllIngredientSuppliersList{ get; set; }
    }

    public class CreateIngredientViewModel : IngredientViewModel
    {
        public IEnumerable<SupplierViewModel> AllSuppliers { get; set; }
    }

    public class IngredientIndexViewModel
    {
        public CreateIngredientViewModel CreateIngredientViewModel { get; set; }
        public IEnumerable<IngredientViewModel> IngredientsList { get; set; }
    }
}