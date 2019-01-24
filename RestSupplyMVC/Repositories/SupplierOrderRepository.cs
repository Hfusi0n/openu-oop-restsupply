using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyMVC.Repositories
{
    public class SupplierOrderRepository : ISupplierOrderRepository
    {
        private readonly RestSupplyDbContext _context;
        public SupplierOrderRepository(RestSupplyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<IngredientOrders> GetAll()
        {
            return _context.IngredientOrdersSet.AsEnumerable();
        }

        public IngredientOrders GetById(int id)
        {
            return _context.IngredientOrdersSet.Find(id);
        }

        public void Add(IngredientOrders order)
        {
            _context.IngredientOrdersSet.Add(order);
        }
    }
}