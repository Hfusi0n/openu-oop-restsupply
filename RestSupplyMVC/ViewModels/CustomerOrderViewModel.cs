using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RestSupplyMVC.ViewModels
{
    public class CustomerOrderViewModel
    { 
        [Required]
        [Display(Name = "Menu Item")]
        public string SelectedMenuItem { get; set; }

        public IEnumerable<SelectListItem> MenuItems { get; set; }
    }
}