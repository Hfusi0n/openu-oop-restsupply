using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Persistence
{
    public class UnitOfWork
    {
        private readonly RestSupplyDbContext _context;

        // set is private becuase we want to init the repository only inside the UoW c-tor
        public SupplierRepository Suppliers { get; private set; }
        public UnitOfWork(RestSupplyDbContext context)
        {
            _context = context;
            Suppliers = new SupplierRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}