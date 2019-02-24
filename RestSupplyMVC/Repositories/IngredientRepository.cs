using RestSupplyDB;
using System.Collections.Generic;
using System.Linq;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly RestSupplyDbContext _context;

        public IngredientRepository(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ingredients> GetAll()
        {
            return _context.IngredientsSet.ToList();
        }

        public IEnumerable<Ingredients> GetIngredientsBySupplierId(int id)
        {
            return _context.IngredientsSet.Where(i => i.SuppliersIngredients.Any(si => si.SupplierId == id));
        }

        public List<Ingredients> GetIngredientsByKitchenId(int kitchenId)
        {
            var ingredients =
                _context.IngredientsSet.Where(i => i.KitchenIngredients.Any(ki => ki.KitchenId == kitchenId)).ToList();
            return ingredients;
        }

        public Ingredients GetById<TKey>(TKey id)
        {
            return _context.IngredientsSet.Find(id);
        }

        public void Add(Ingredients item)
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