using RestSupplyDB.Models.Kitchen;

namespace RestSupplyDB.Models.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerOrders")]
    public partial class CustomerOrders
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int KitchenId { get; set; }

        public virtual Kitchens Kitchens { get; set; }        
    }
}
