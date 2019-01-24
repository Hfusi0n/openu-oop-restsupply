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

        public DBModels.Ingredients GetById(int id)
        {
            return _context.IngredientsSet.Find(id);
        }

        public void Add(DBModels.Ingredients item)
        {
            _context.IngredientsSet.Add(item);
        }

        public void Remove(DBModels.Ingredients ingredients)
        {
            throw new System.NotImplementedException();
        }
    }
}