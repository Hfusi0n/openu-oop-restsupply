using System.Collections.Generic;

namespace RestSupplyMVC.ViewModels
{
    public class KitchenIngredientViewModel
    {
        public int? KitchenIngredientId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double? MinimalQuantity { get; set; }
        public double? CurrentQuantity { get; set; }
        public string Unit { get; set; }
        public int KitchenId { get; set; }
        public string KitchenName { get; set; }
    }

    public class KitchenIngredientIndexViewModel
    {
        public List<KitchenIngredientViewModel> KitchenIngredientsList { get; set; }
    }
}