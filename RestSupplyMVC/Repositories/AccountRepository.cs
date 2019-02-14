using RestSupplyDB;
using RestSupplyDB.Models.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSupplyMVC.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<AppUser> GetAll();
        AppUser GetById<TKey>(TKey id);
        IEnumerable<AppRole> GetAppRoles();
    }

    public class AccountRepository : IAccountRepository
    {
        private IEnumerable<AppRole> Roles { get; set; }

        private readonly RestSupplyDbContext _context;
        public AccountRepository(RestSupplyDbContext context)
        {
            _context = context;
            Roles = new List<AppRole>();
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.Users.ToList();            
        }
        
        public AppUser GetById<TKey>(TKey id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<AppRole> GetAppRoles()
        {
            // Don't get the roles from the database for no reason, use the cached data
            // The Roles list shouldn't change during the active session of the database context
            if (!Roles.Any())
            {
                // No cache data, get the roles list from the database
                Roles = _context.Roles.ToList();
            }

            return Roles;
        }
    }
}