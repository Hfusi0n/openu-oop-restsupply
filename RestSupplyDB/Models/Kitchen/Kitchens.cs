using RestSupplyDB.Models.Customer;

namespace RestSupplyDB.Models.Kitchen
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KitchensSet")]
    public partial class Kitchens
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kitchens()
        {
            KitchenIngredientsSet = new HashSet<KitchenIngredients>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<KitchenIngredients> KitchenIngredientsSet { get; set; }
        
        public virtual ICollection<CustomerOrders> CustomerOrdersSet { get; set; }
    }
}
