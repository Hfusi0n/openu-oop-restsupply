using System.ComponentModel.DataAnnotations.Schema;
using RestSupplyDB.Models.Menu;

namespace RestSupplyDB.Models.Customer
{
    [Table("CustomerDetailOrders")]
    public class CustomerDetailOrders
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        public virtual MenuItems MenuItems { get; set; }

    }
}
