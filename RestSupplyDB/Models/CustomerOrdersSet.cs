namespace RestSupplyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerOrdersSet")]
    public partial class CustomerOrdersSet
    {
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        public int KitchenId { get; set; }

        public virtual KitchensSet KitchensSet { get; set; }        
    }
}
