using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public class IngredientRepository
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

        public Ingredients GetById(int id)
        {
            return _context.IngredientsSet.Find(id);
        }

        public void Add(Ingredients item)
        {
            _context.IngredientsSet.Add(item);
        }
    }
}