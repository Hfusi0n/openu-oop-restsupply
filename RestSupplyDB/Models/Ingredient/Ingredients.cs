using RestSupplyDB.Models.Kitchen;
using RestSupplyDB.Models.Menu;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyDB.Models.Ingredient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ingredients")]
    public partial class Ingredients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingredients()
        {
            KitchenIngredients = new HashSet<KitchenIngredients>();
            MenuIngredients = new HashSet<MenuIngredients>();
            IngredientListOrders = new HashSet<IngredientListOrders>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Unit { get; set; }

        public virtual ICollection<Supplier.Supplier> Suppliers { get; set; }
        
        public virtual ICollection<KitchenIngredients> KitchenIngredients { get; set; }
        
        public virtual ICollection<MenuIngredients> MenuIngredients { get; set; }
        
        public virtual ICollection<IngredientListOrders> IngredientListOrders { get; set; }
    }
}
