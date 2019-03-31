﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB;
using RestSupplyDB.Migrations;
using RestSupplyDB.Models.Kitchen;
using RestSupplyMVC.Helpers;

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

        public Kitchens GetById<TKey>(TKey id)
        {
            return _context.KitchensSet.Find(id);
        }

        public void Add(Kitchens kitchen)
        {
            _context.KitchensSet.Add(kitchen);

            // Assign the new kitchen to all admins and chain managers
            var adminRoleId = _context.Roles.First(r => r.Name == Role.Admin).Id;
            var chainManagerRoleId = _context.Roles.First(r => r.Name == Role.ChainManager).Id;
            var usersToAddKitchen = _context.Users
                .Where(u => u.Roles.Any(r => r.RoleId == adminRoleId || r.RoleId == chainManagerRoleId)).ToList();

            AddUsersToKitchen(kitchen.Id, usersToAddKitchen.Select(u => u.Id).ToList());

        }

        public void Remove(Kitchens item)
        {
            // Delete the entry
            _context.KitchensSet.Remove(item);
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

        public List<Kitchens> GetKitchensByUserId(string userId)
        {
            if(userId == null)
                return new List<Kitchens>();

            var kitchens = _context.KitchensSet.Where(k => k.KitchenUsers.Any(ku => ku.UserId == userId)).ToList();

            return kitchens;
        }

        public void RemoveUserFromKitchen(int kitchenId, string userId)
        {
            var dbKitchen = _context.KitchensSet.Find(kitchenId);

            var kitchenUserToRemove = dbKitchen?.KitchenUsers.FirstOrDefault(ku => ku.UserId == userId);
            dbKitchen?.KitchenUsers.Remove(kitchenUserToRemove);
        }
    }
}