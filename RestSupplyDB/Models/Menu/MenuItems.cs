using RestSupplyDB.Models.Customer;

namespace RestSupplyDB.Models.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuItemsSet")]
    public partial class MenuItems
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuItems()
        {
            MenuIngredientsSet = new HashSet<MenuIngredients>();
            CustomerDetailOrdersSet = new HashSet<CustomerDetailOrders>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public virtual ICollection<MenuIngredients> MenuIngredientsSet { get; set; }
        
        public virtual ICollection<CustomerDetailOrders> CustomerDetailOrdersSet { get; set; }
    }
}
