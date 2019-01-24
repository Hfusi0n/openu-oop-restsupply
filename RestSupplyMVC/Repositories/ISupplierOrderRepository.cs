using System.Collections.Generic;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public interface ISupplierOrderRepository
    {
        IEnumerable<IngredientOrders> GetAll();
        IngredientOrders GetById(int id);
        void Add(IngredientOrders order);
    }
}