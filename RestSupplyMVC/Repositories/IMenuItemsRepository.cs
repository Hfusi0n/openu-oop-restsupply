using System.Collections.Generic;
using RestSupplyDB.Models.Menu;

namespace RestSupplyMVC.Repositories
{
    public interface IMenuItemsRepository : IRepository<MenuItems>
    {
        void RemoveMenuItemIngredient(int menuItemId, int ingredientId);
        void AddMenuItemIngredients(int menuItemId, List<MenuItemIngredients> menuItemIngredients);
    }
}