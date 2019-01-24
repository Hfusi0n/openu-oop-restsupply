using System;
using System.Collections.Generic;
using RestSupplyDB;
using RestSupplyDB.Models.Menu;

namespace RestSupplyMVC.Repositories
{
    public class MenuItemIngredientsRepository : IMenuItemIngredientsRepository
    {
        private readonly RestSupplyDbContext _context;
        public MenuItemIngredientsRepository(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MenuItemIngredients> AddBulk(IEnumerable<MenuItemIngredients> items)
        {
            return _context.MenuIngredientsSet.AddRange(items);
        }
    }
}