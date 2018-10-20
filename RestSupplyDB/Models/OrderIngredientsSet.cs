namespace RestSupplyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderIngredientsSet")]
    public partial class OrderIngredientsSet
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? IngredientId { get; set; }

        public double IngredientPrice { get; set; }

        public int? SupplierId { get; set; }

        public virtual IngredientsSet IngredientsSet { get; set; }

        public virtual OrdersSet OrdersSet { get; set; }

        public virtual SuppliersSet SuppliersSet { get; set; }
    }
}
