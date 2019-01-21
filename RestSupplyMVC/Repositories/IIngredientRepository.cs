using System.Collections.Generic;

namespace RestSupplyMVC.Repositories
{
    public interface IIngredientRepository
    {
        IEnumerable<RestSupplyDB.Models.Ingredient.Ingredients> GetAll();
        RestSupplyDB.Models.Ingredient.Ingredients GetById(int id);
        void Add(RestSupplyDB.Models.Ingredient.Ingredients item);
        void Remove(RestSupplyDB.Models.Ingredient.Ingredients ingredients);
    }
}