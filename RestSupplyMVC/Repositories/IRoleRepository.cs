using System.Collections.Generic;
using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Repositories
{
    public interface IRoleRepository : IRepository<AppRole>
    {
        List<AppUserRole> GetRolesByUserId(string userId);
        string GetRolesNamesAsStringByUserId(string userId);
    }
}