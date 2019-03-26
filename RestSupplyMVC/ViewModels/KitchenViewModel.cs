using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace RestSupplyMVC.ViewModels
{
    public class KitchenViewModel
    {
        public int KitchenId { get; set; }
        public string KitchenName { get; set; }
        public string KitchenAddress { get; set; }
        public List<UserViewModel> KitchenUsersList { get; set; }
    }
}