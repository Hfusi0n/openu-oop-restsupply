using System.Collections.Generic;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredients>
    {
        void Remove(Ingredients ingredients);
        IEnumerable<Ingredients> GetIngredientsBySupplierId(int id);
    }
}