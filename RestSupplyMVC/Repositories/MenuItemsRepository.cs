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

        public MenuItems GetById<TKey>(TKey id)
        {
            return _context.MenuItemsSet.Find(id);
        }

        public void Add(MenuItems item)
        {
            _context.MenuItemsSet.Add(item);
        }

        public void Remove<TKey>(TKey id)
        {
            // Get the item...
            var item = _context.MenuItemsSet.Find(id);

            // Remove the entry
            _context.MenuItemsSet.Remove(item);
        }
    }
}