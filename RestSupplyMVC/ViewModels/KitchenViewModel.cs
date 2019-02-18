using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSupplyMVC.ViewModels
{
    public class KitchenViewModel
    {
        public int KitchenId { get; set; }
        public string KitchenName { get; set; }
        public string KitchenAddress { get; set; }
        public List<UserViewModel> AllUsersList { get; set; }
        public List<UserViewModel> KitchenUsersList { get; set; }

        // Only users that are not associated with the current kitchen
        public List<UserViewModel> NotAssociatedUsers
        {
            get { return AllUsersList.Where(i => KitchenUsersList.All(si => si.Id != i.Id)).ToList(); }

        }
    }
}