using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace RestSupplyMVC.ViewModels
{
    public class CustomerOrderViewModel
    {
        public int CustomerOrderId { get; set; }
        public int KitchenId { get; set; }
        public string KitchenName { get; set; }

        public IEnumerable<CustomerOrderDetailViewModel> CustomerOrderDetailsList { get; set; }
    }

    public class CustomerOrderDetailViewModel : MenuItemViewModel
    {
        public int CustomerOrderDetailId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateCustomerOrderViewModel : CustomerOrderViewModel
    {
        public Dictionary<MenuItemViewModel, int> AllMenuItemsToQuantityMap { get; set; }
    }

}