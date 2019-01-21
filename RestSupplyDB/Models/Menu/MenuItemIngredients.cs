using RestSupplyDB.Models.Ingredient;

namespace RestSupplyDB.Models.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuIngredients")]
    public partial class MenuItemIngredients
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int IngredientId { get; set; }
        [Required]
        public int MenuItemId { get; set; }

        public double Quantity { get; set; }

        public virtual Ingredients IngredientsSet { get; set; }

        public virtual MenuItems MenuItemsSet { get; set; }
    }
}
