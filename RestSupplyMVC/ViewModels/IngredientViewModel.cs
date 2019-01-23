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
        [Display(Name = "שם חומר גלם")] public string Name { get; set; }
        public int Id { get; set; }
        [Display(Name = "יחידת מידה")] public string Unit { get; set; }
    }
}