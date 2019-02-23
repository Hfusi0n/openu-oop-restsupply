using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestSupplyMVC.ViewModels
{
    public class KitchenIngredientViewModel
    {
        public int? KitchenIngredientId { get; set; }
        public int IngredientId { get; set; }
        [Display(Name = "Ingredient Name")]
        public string IngredientName { get; set; }
        [Display(Name = "Minimal Quantity")]
        public double? MinimalQuantity { get; set; }
        [Display(Name = "Current Quantity")]
        public double? CurrentQuantity { get; set; }
        public string Unit { get; set; }
        public int KitchenId { get; set; }
        [Display(Name = "Kitchen Name")]
        public string KitchenName { get; set; }
    }

    public class KitchenIngredientIndexViewModel
    {
        public List<KitchenIngredientViewModel> KitchenIngredientsList { get; set; }
    }
}