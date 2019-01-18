using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace RestSupplyMVC.ViewModels
{
    public class CustomerOrderViewModel
    {
        public IEnumerable<SelectListItem> MenuItems { get; set; }

        public Dictionary<string, string> Orders { get; set; }
    }

    
}