namespace RestSupplyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuItemsSet")]
    public partial class MenuItemsSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuItemsSet()
        {
            MenuIngredientsSet = new HashSet<MenuIngredientsSet>();
            CustomerDetailOrdersSet = new HashSet<CustomerDetailOrdersSet>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public virtual ICollection<MenuIngredientsSet> MenuIngredientsSet { get; set; }
        
        public virtual ICollection<CustomerDetailOrdersSet> CustomerDetailOrdersSet { get; set; }
    }
}
