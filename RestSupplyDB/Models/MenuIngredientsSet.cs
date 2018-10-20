namespace RestSupplyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuIngredientsSet")]
    public partial class MenuIngredientsSet
    {
        public int Id { get; set; }

        public bool Valid { get; set; }

        [Required]
        public string IngredientName { get; set; }

        public int? IngredientId { get; set; }

        public int? MenuItemId { get; set; }

        public double Quantity { get; set; }

        public virtual IngredientsSet IngredientsSet { get; set; }

        public virtual MenuItemsSet MenuItemsSet { get; set; }
    }
}
