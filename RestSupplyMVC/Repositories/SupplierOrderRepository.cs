using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyDB.Models.Ingredient;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public class SupplierOrderRepository : ISupplierOrderRepository
    {
        private readonly RestSupplyDbContext _context;
        public SupplierOrderRepository(RestSupplyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SupplierOrders> GetAll()
        {
            return _context.IngredientOrdersSet.ToList();
        }

        public SupplierOrders GetById<TKey>(TKey id)
        {
            return _context.IngredientOrdersSet.Find(id);
        }

        public List<SupplierOrders> GetAllByKitchenId(int kitchenId)
        {
            return _context.IngredientOrdersSet.Where(c => c.KitchenId == kitchenId).ToList();
        }

        public void Add(SupplierOrders order)
        {
            _context.IngredientOrdersSet.Add(order);
        }

        public void Remove(SupplierOrders item)
        {
            throw new NotImplementedException();
        }
    }
}