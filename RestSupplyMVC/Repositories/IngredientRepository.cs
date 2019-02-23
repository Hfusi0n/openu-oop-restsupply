using RestSupplyDB;
using System.Collections.Generic;
using System.Linq;
using DBModels = RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly RestSupplyDbContext _context;

        public IngredientRepository(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DBModels.Ingredients> GetAll()
        {
            return _context.IngredientsSet.ToList();
        }

        public IEnumerable<DBModels.Ingredients> GetIngredientsBySupplierId(int id)
        {
            return _context.IngredientsSet.Where(i => i.SuppliersIngredients.Any(si => si.SupplierId == id));
        }

        public DBModels.Ingredients GetById<TKey>(TKey id)
        {
            return _context.IngredientsSet.Find(id);
        }

        public void Add(DBModels.Ingredients item)
        {
            _context.IngredientsSet.Add(item);
        }

        public void Remove<TKey>(TKey id)
        {
            // Get the item...
            var item = _context.IngredientsSet.Find(id);

            // Remove the entry
            _context.IngredientsSet.Remove(item);
        }
    }
}