using RestSupplyDB.Models.Ingredient;

namespace RestSupplyDB.Models.Supplier
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Suppliers")]
    public partial class Suppliers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suppliers()
        {
            IngredientsSet = new HashSet<Ingredients>();
            IngredientOrdersSet = new HashSet<IngredientOrders>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<Ingredients> IngredientsSet { get; set; }
        
        public virtual ICollection<IngredientOrders> IngredientOrdersSet { get; set; }
    }
}
