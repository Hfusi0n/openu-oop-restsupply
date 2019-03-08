using System;
using System.Collections.Generic;
using System.Linq;
using RestSupplyDB;
using RestSupplyDB.Models.Customer;

namespace RestSupplyMVC.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly RestSupplyDbContext _context;

        public CustomerOrderRepository(RestSupplyDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<CustomerOrders> GetAll()
        {
            return _context.CustomerOrdersSet.ToList();
        }

        public CustomerOrders GetById<TKey>(TKey id)
        {
            return _context.CustomerOrdersSet.Find(id);
        }

        public void Add(CustomerOrders item)
        {
            _context.CustomerOrdersSet.Add(item);
        }

        public void Remove(CustomerOrders item)
        {
            throw new NotImplementedException();
        }

        public List<CustomerOrders> GetAllByKitchenId(int kitchenId)
        {
            return _context.CustomerOrdersSet.Where(c => c.KitchenId == kitchenId).ToList();
        }
    }
}