using RestSupplyDB.Models.Ingredient;

namespace RestSupplyDB.Models.Kitchen
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KitchenIngredientsSet")]
    public partial class KitchenIngredients
    {
        public int Id { get; set; }

        public bool Valid { get; set; }

        public int KitchenId { get; set; }

        public int IngredientId { get; set; }

        public double MinimalQuantity { get; set; }

        public double CurrentQuantity { get; set; }

        public virtual Ingredients IngredientsSet { get; set; }

        public virtual Kitchens KitchensSet { get; set; }
    }
}
