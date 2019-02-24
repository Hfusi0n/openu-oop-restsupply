using System.Collections.Generic;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredients>
    {
        IEnumerable<Ingredients> GetIngredientsBySupplierId(int id);
        List<Ingredients> GetIngredientsByKitchenId(int kitchenId);
    }
}