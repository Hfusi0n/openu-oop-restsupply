using System.Collections.Generic;
using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.DTOs;

namespace RestSupplyMVC.Repositories
{
    public interface IAccountRepository
    {
        List<AppUserDTO> GetAll();
        List<AppRole> GetAppRoles();
        AppUserDTO GetById(string id);
    }
}