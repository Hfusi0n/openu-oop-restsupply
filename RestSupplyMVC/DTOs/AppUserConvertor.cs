using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSupplyDB.Models.AppUser;
using RestSupplyDB.Models.Kitchen;

namespace RestSupplyMVC.DTOs
{
    public class AppUserConvertor
    {
        public static AppUser ToDomainModel(AppUserDTO dtoModel)
        {
            return new AppUser
            {
                UserKitchens = (ICollection<KitchenUsers>) dtoModel.Kitchens,
                FirstName = dtoModel.FirstName,
                LastName = dtoModel.LastName,
                Email = dtoModel.Email,
                Id = dtoModel.UserId

                // TODO Convert Role from dto
            };
        }

        public static AppUserDTO ToDto(AppUser domainModel)
        {
            return new AppUserDTO
            {
                Email = domainModel.Email,
                FirstName = domainModel.FirstName,
                LastName = domainModel.LastName,
                UserId = domainModel.Id,
                Kitchens = domainModel.UserKitchens,
                Role = domainModel.Roles.FirstOrDefault()
            };
        }
    }
}