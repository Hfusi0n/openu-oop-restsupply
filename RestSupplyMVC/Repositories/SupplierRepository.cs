using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly RestSupplyDbContext _context;
        public SupplierRepository(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.SuppliersSet.ToList();
        }

        public Supplier GetById(int id)
        {
            return _context.SuppliersSet.Find(id);
        }

        public void Add(Supplier supplier)
        {
            _context.SuppliersSet.Add(supplier);
        }

        public void AddSupplierIngredients(int supplierId, List<int> ingredientIdsList)
        {
            var dbSupplier = _context.SuppliersSet.Find(supplierId);
            foreach (var ingredientId in ingredientIdsList)
            {
                dbSupplier?.SuppliersIngredients.Add(
                    new SuppliersIngredients
                    {
                        IngredientId = ingredientId
                    });
            }
        }
    }
}