using System.Collections.Generic;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetAll();
        Supplier GetById(int id);
        void Add(Supplier supplier);
        void AddSupplierIngredients(int supplierId, List<int> ingredientIdsList);
    }
}