using System.Collections.Generic;
using RestSupplyDB.Models.Kitchen;

namespace RestSupplyMVC.Repositories
{
    public interface IKitchenIngredientRepository : IRepository<KitchenIngredients>
    {
        Dictionary<int, KitchenIngredients> GetIngredientIdToKitchenIngredientMap(int kitchenId);

    }
}