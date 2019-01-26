using RestSupplyDB.Models.Supplier;

namespace RestSupplyDB.Models.Supplier
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        public string Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier SupplierSet { get; set; }
        
        public virtual ICollection<SupplierOrderDetails> SupplierOrderDetails { get; set; }
    }
}
