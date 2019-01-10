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
        [Display(Name = "שם ספק")]
        public string Name { get; set; }
        [Display(Name = "כתובת")]
        public string Address { get; set; }
        [Display(Name = "טלפון")]
        public string Phone { get; set; }
        [Display(Name = "חומר גלם")]
        public string Ingredient { get; set; }
        [Display(Name = "רשימת חומרי גלם")]
        public IEnumerable<Ingredients> IngredientList { get; set; }
    }
}