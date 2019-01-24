using System.Collections.Generic;
using RestSupplyDB.Models.Menu;

namespace RestSupplyMVC.Repositories
{
    public interface IMenuItemIngredientsRepository
    {
        IEnumerable<MenuItemIngredients> AddBulk(IEnumerable<MenuItemIngredients> items);
    }
}