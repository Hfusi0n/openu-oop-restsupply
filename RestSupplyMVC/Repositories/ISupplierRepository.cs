using System.Collections.Generic;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {      
        void AddSupplierIngredients(int supplierId, List<int> ingredientIdsList);
        void RemoveSupplierIngredient(int supplierId, int ingredientId);
    }
}