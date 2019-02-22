using RestSupplyDB;
using RestSupplyDB.Models.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyMVC.DTOs;

namespace RestSupplyMVC.Repositories
{
    public interface IAccountRepository
    {
        List<AppUserDTO> GetAll();
        IEnumerable<AppRole> GetAppRoles();
         AppUserDTO GetById(string id);
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

        public List<AppUserDTO> GetAll()
        {
            var appUsers = new List<AppUserDTO>();
            foreach (var appUser in _context.Users)
            {
                appUsers.Add(AppUserConvertor.ToDto(appUser));
            }

            return appUsers;
        }
        

        public AppUserDTO GetById(string id)
        {
            var dbUser = _context.Users.FirstOrDefault(user => user.Id == id);
            return AppUserConvertor.ToDto(dbUser);
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