namespace RestSupplyDB.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KitchenIngredientsSet")]
    public partial class KitchenIngredientsSet
    {
        public int Id { get; set; }

        public bool Valid { get; set; }

        public int KitchenId { get; set; }

        public int IngredientId { get; set; }

        public double MinimalQuantity { get; set; }

        public double CurrentQuantity { get; set; }

        public virtual IngredientsSet IngredientsSet { get; set; }

        public virtual KitchensSet KitchensSet { get; set; }
    }
}
