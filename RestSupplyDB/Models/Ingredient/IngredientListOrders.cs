namespace RestSupplyDB.Models.Ingredient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IngredientListOrders")]
    public partial class IngredientListOrders
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int IngredientId { get; set; }

        public double MoneyAmount { get; set; }

        public virtual Ingredients IngredientsSet { get; set; }

        public virtual IngredientOrders OrdersSet { get; set; }

    }
}
