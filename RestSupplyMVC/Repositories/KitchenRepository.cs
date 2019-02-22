using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyDB.Migrations;
using RestSupplyDB.Models.Kitchen;

namespace RestSupplyMVC.Repositories
{
    public class KitchenRepository : IKitchenRepository
    {
        private readonly RestSupplyDbContext _context;
        public KitchenRepository(RestSupplyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Kitchens> GetAll()
        {
            return _context.KitchensSet.ToList();
        }

        public Kitchens GetById(int id)
        {
            return _context.KitchensSet.Find(id);
        }

        public void Add(Kitchens kitchen)
        {
            _context.KitchensSet.Add(kitchen);
        }

        public void AddUserToKitchen(int kitchenId, string userId)
        {
            var dbKitchen = _context.KitchensSet.Find(kitchenId);
            dbKitchen?.KitchenUsers.Add(new KitchenUsers
            {
                UserId = userId
            });
        }

        public void AddUsersToKitchen(int kitchenId, List<string> userIds)
        {
            var dbKitchen = _context.KitchensSet.Find(kitchenId);
            foreach (var userId in userIds)
            {
                dbKitchen?.KitchenUsers.Add(new KitchenUsers
                {
                    UserId = userId
                });
            }

        }

        public void RemoveUserFromKitchen(int kitchenId, string userId)
        {
            var dbKitchen = _context.KitchensSet.Find(kitchenId);

            var kitchenUserToRemove = dbKitchen?.KitchenUsers.FirstOrDefault(ku => ku.UserId == userId);
            dbKitchen?.KitchenUsers.Remove(kitchenUserToRemove);
        }
    }
}