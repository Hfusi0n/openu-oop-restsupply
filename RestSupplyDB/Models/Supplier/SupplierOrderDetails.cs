using RestSupplyDB.Models.Ingredient;

namespace RestSupplyDB.Models.Supplier
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SupplierOrderDetails")]
    public partial class SupplierOrderDetails
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int IngredientId { get; set; }

        public double Amount { get; set; }

        public virtual Ingredients IngredientsSet { get; set; }

        public virtual SupplierOrders OrdersSet { get; set; }

    }
}
