using System.ComponentModel.DataAnnotations.Schema;

namespace RestSupplyDB.Models
{
    [Table("CustomerDetailOrdersSet")]
    public class CustomerDetailOrdersSet
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        public virtual MenuItemsSet MenuItemsSet { get; set; }

    }
}
