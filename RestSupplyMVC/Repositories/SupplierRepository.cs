using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public class SupplierRepository
    {
        private readonly RestSupplyDBModel _context;
        public SupplierRepository(RestSupplyDBModel context)
        {
            _context = context;
        }

        public IEnumerable<Suppliers> GetAllSuppliers()
        {
            return _context.SuppliersSet.ToList();
        }
        // TODO all saveChanges should be done be UnitOfWork.Complete();
    }
}