using RestSupplyDB.Models.Kitchen;

namespace RestSupplyDB.Models.Supplier
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SupplierOrders")]
    public partial class SupplierOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierOrders()
        {
            SupplierOrderDetails = new HashSet<SupplierOrderDetails>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int SupplierId { get; set; }

        public int KitchenId { get; set; }

        //public virtual Kitchens Kitchen { get; set; }

        public virtual Supplier SupplierSet { get; set; }
        
        public virtual ICollection<SupplierOrderDetails> SupplierOrderDetails { get; set; }
    }
}
