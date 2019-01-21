using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace RestSupplyMVC.ViewModels
{
    public class CustomerOrderViewModel
    {
        [Display(Name = "מנות מהתפריט")]
        public IEnumerable<SelectListItem> MenuItems { get; set; }
        [Display(Name = "הזמנות לקוח")]

        public Dictionary<string, string> Orders { get; set; }
    }

    
}