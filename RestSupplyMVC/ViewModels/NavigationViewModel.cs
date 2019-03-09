using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSupplyMVC.ViewModels
{
    public class NavigationViewModel
    {
        public int CurrentUserKitchenId { get; set; }
        public List<KitchenViewModel> UserKitchensList { get; set; }
        public bool IsSingleKitchen => UserKitchensList.Count == 1;
        public bool ShowActions { get; set; }
    }
}