namespace RestSupplyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IngredientListOrdersSet")]
    public partial class IngredientListOrdersSet
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int IngredientId { get; set; }

        public double IngredientPrice { get; set; }

        public virtual IngredientsSet IngredientsSet { get; set; }

        public virtual IngredientOrdersSet OrdersSet { get; set; }

    }
}
