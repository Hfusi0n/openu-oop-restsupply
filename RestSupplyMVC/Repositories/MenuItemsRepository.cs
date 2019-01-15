using RestSupplyDB;
using System.Collections.Generic;
using System.Linq;
using DBModels = RestSupplyDB.Models.Menu;

namespace RestSupplyMVC.Repositories
{
    public class MenuItemsReposiotry
    {
        private readonly RestSupplyDbContext _context;

        public MenuItemsReposiotry(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DBModels.MenuItems> GetAll()
        {

            var itemsList = _context.MenuItemsSet.ToList();
            return itemsList;
        }

        public DBModels.MenuItems GetById(int id)
        {
            return _context.MenuItemsSet.Find(id);
        }

        public void Add(DBModels.MenuItems item)
        {
            _context.MenuItemsSet.Add(item);
        }
    }
}