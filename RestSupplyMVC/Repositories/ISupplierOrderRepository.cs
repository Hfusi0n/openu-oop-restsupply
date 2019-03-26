using System.Collections.Generic;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public interface ISupplierOrderRepository : IRepository<SupplierOrders>
    {
        List<SupplierOrders> GetAllByKitchenId(int kitchenId);



    }
}