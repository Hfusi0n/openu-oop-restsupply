using System.Collections.Generic;
using RestSupplyDB.Models.Menu;

namespace RestSupplyMVC.Repositories
{
    public interface IMenuItemsRepository
    {
        IEnumerable<RestSupplyDB.Models.Menu.MenuItems> GetAll();
        RestSupplyDB.Models.Menu.MenuItems GetById(int id);
        MenuItems Add(MenuItems item);
    }
}