using System.Collections.Generic;

namespace RestSupplyMVC.Repositories
{
    public interface IMenuItemsRepository
    {
        IEnumerable<RestSupplyDB.Models.Menu.MenuItems> GetAll();
        RestSupplyDB.Models.Menu.MenuItems GetById(int id);
        void Add(RestSupplyDB.Models.Menu.MenuItems item);
    }
}