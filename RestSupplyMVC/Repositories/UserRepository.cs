using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using RestSupplyDB;
using RestSupplyDB.Models.AppUser;
using RestSupplyMVC.Helpers;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Repositories

{
    public class UserRepository : IUserRepository
    {
        private readonly RestSupplyDbContext _context;
        private readonly AppUserManager _userManager;
        private readonly KitchenRepository _kitchenRepository;

        public UserRepository(RestSupplyDbContext context)
        {
            _userManager = new AppUserManager(new AppUserStore(context));
            _kitchenRepository = new KitchenRepository(context);
            _context = context;
        }
        public IEnumerable<AppUser> GetAll()
        {
            var dbUsers = _context.Users.ToList();
            return dbUsers;
            
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

        public void AddRoleToUser(string userId, string roleName)
        {
            _userManager.AddToRole(userId, roleName);

            // assigning Admin and Chain Manager all the kitchens in the system
            if (roleName == Role.Admin || roleName == Role.ChainManager)
            {
                var allKitchens = _context.KitchensSet.ToList();
                var userKitchens = GetById(userId).UserKitchens.ToList();
                foreach (var kitchen in allKitchens)
                {
                    if (userKitchens.All(k => k.KitchenId != kitchen.Id))
                        _kitchenRepository.AddUserToKitchen(kitchen.Id,userId);
                }
            }
        }

        // OUT OF SCOPE
        public void Remove(AppUser item)
        {
            throw new System.NotImplementedException();
        }
    }
}