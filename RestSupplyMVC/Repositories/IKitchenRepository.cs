using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSupplyDB;
using RestSupplyDB.Models.Kitchen;
using RestSupplyDB.Models.Supplier;

namespace RestSupplyMVC.Repositories
{
    public interface IKitchenRepository : IRepository<Kitchens>
    {
        void AddUserToKitchen(int kitchenId, string userId);
        void RemoveUserFromKitchen(int kitchenId, string userId);
        void AddUsersToKitchen(int kitchenId, List<string> userIds);
    }
}
