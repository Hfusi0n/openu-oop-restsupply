using RestSupplyDB;
using System.Collections.Generic;
using System.Linq;
using RestSupplyMVC.ViewModels;
using RestSupplyDB.Models.Menu;

namespace RestSupplyMVC.Repositories
{
    public class MenuItemsRepository : IMenuItemsRepository
    {
        private readonly RestSupplyDbContext _context;

        public MenuItemsRepository(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MenuItems> GetAll()
        {
            return _context.MenuItemsSet.ToList();
        }

        public MenuItems GetById(int id)
        {
            return _context.MenuItemsSet.Find(id);
        }

        public MenuItems Add(MenuItems item)
        {
            return _context.MenuItemsSet.Add(item);
        }

    }
}