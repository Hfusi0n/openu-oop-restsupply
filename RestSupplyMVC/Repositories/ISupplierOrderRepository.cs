using System.Collections.Generic;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public interface ISupplierOrderRepository
    {
        IEnumerable<SupplierOrders> GetAll();
        SupplierOrders GetById(int id);
        void Add(SupplierOrders order);
    }
}