using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Repositories
{
    public interface IUserRepository : IRepository<AppUser>
    {
        void AddRoleToUser(string userId, string roleName);
    }
}