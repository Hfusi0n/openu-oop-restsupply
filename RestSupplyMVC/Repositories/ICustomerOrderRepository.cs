using System.Collections.Generic;
using RestSupplyDB.Models.Customer;

namespace RestSupplyMVC.Repositories
{
    public interface ICustomerOrderRepository : IRepository<CustomerOrders>
    {
        List<CustomerOrders> GetAllByKitchenId(int kitchenId);
    }
}