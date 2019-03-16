using System.Collections.Generic;
using System.Linq;
using RestSupplyDB;
using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Repositories

{
    public class UserRepository : IUserRepository
    {
        private readonly RestSupplyDbContext _context;
        public UserRepository(RestSupplyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<AppUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public AppUser GetById<TKey>(TKey id)
        {
            var dbUser = _context.Users.Find(id);
            return dbUser;
        }

        public void Add(AppUser item)
        {
            _context.Users.Add(item);
        }

        // OUT OF SCOPE
        public void Remove(AppUser item)
        {
            throw new System.NotImplementedException();
        }
    }
}