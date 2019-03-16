using System.Collections.Generic;
using System.Linq;
using RestSupplyDB;
using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RestSupplyDbContext _context;

        public RoleRepository(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AppRole> GetAll()
        {
            var roles = _context.Roles.ToList();
            return roles;
        }

        public AppRole GetById<TKey>(TKey id)
        {
            throw new System.NotImplementedException();
        }

        // OUT OF SCOPE
        public void Add(AppRole item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(AppRole item)
        {
            throw new System.NotImplementedException();
        }

        public List<AppUserRole> GetRolesByUserId(string userId)
        {
            var roles = _context.Users.Find(userId).Roles.ToList();
            
            return roles;
        }

    }
}