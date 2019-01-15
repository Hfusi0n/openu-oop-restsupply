using RestSupplyDB;
using System.Collections.Generic;
using System.Linq;
using DBModels = RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public class IngredientRepository
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

        public DBModels.Ingredients GetById(int id)
        {
            return _context.IngredientsSet.Find(id);
        }

        public void Add(DBModels.Ingredients item)
        {
            _context.IngredientsSet.Add(item);
        }
    }
}