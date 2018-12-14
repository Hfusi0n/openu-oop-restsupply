using RestSupplyDB.Models.Supplier;

namespace RestSupplyDB.Models.Ingredient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IngredientOrders")]
    public partial class IngredientOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IngredientOrders()
        {
            IngredientListOrdersSet = new HashSet<IngredientListOrders>();
        }

        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        public int SupplierId { get; set; }

        public virtual Suppliers SuppliersSet { get; set; }
        
        public virtual ICollection<IngredientListOrders> IngredientListOrdersSet { get; set; }
    }
}
