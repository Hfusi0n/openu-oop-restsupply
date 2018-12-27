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

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _context.SuppliersSet.ToList();
        }

        public Supplier GetSupplierById(int id)
        {
            return _context.SuppliersSet.Find(id);
        } 
        // TODO all saveChanges should be done be UnitOfWork.Complete();
        public void AddSupplier(Supplier supplier)
        {
            _context.SuppliersSet.Add(supplier);
        }
    }
}