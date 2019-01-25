using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.ViewModels
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Display(Name = "שם ספק")]
        public string Name { get; set; }
        [Display(Name = "כתובת")]
        public string Address { get; set; }
        [Display(Name = "טלפון")]
        public string Phone { get; set; }
        [Display(Name = "רשימת חומרי גלם")]
        public IEnumerable<IngredientViewModel> AllSupplierIngredientsList { get; set; }


    }
    public class CreateSupplierViewModel : SupplierViewModel
    {
        public IEnumerable<IngredientViewModel> AllIngredients { get; set; }
        //public IEnumerable<IngredientViewModel> SelectedIngredientsForSupplier { get; set; }

    }

    public class SupplierIndexViewModel
    {
        public IEnumerable<SupplierViewModel> SuppliersList { get; set; }
        public CreateSupplierViewModel CreateSupplierViewModel { get; set; }
    }
}