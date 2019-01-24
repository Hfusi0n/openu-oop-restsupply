using System.Collections.Generic;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public interface IIngredientRepository
    {
        IEnumerable<Ingredients> GetAll();
        Ingredients GetById(int id);
        void Add(Ingredients item);
        void Remove(Ingredients ingredients);
        IEnumerable<Ingredients> GetIngredientsBySupplierId(int id);
    }
}