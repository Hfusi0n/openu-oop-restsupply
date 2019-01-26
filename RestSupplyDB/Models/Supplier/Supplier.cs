using RestSupplyDB.Models.Ingredient;

namespace RestSupplyDB.Models.Supplier
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Suppliers")]
    public partial class Supplier 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            SuppliersIngredients = new HashSet<SuppliersIngredients>();
            IngredientOrdersSet = new HashSet<SupplierOrders>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<SuppliersIngredients> SuppliersIngredients { get; set; }
        
        public virtual ICollection<SupplierOrders> IngredientOrdersSet { get; set; }
    }
}
