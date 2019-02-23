using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Kitchen;

namespace RestSupplyMVC.Repositories
{
    public class KitchenIngredientRepository : IKitchenIngredientRepository
    {
        private readonly RestSupplyDbContext _context;
        public KitchenIngredientRepository(RestSupplyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<KitchenIngredients> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public KitchenIngredients GetById<TKey>(TKey id)
        {
            var res = _context.KitchenIngredientsSet.Find(id);
            return res;
        }



        public void Add(KitchenIngredients item)
        {
            throw new System.NotImplementedException();
        }
        
        public void Remove<TKey>(TKey id)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, KitchenIngredients> GetIngredientIdToKitchenIngredientMap(int kitchenId)
        {
            var kitchenIngredients = _context.KitchenIngredientsSet.Include(k => k.IngredientsSet).Include(k => k.KitchensSet).Where(k => k.KitchenId == kitchenId);

            var dictionary = new Dictionary<int,KitchenIngredients>();
            foreach (var ingredient in kitchenIngredients)
            {
                dictionary.Add(ingredient.IngredientId, ingredient);
            }

            return dictionary;

        }
    }
}