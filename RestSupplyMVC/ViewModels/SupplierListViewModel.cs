using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestSupplyMVC.ViewModels
{
    public class SupplierListViewModel
    {
        [Display(Name = "רשימת ספקים")]

        public List<SupplierViewModel> SupplierList { get; set; }

        public SupplierListViewModel()
        { 
            SupplierList = new List<SupplierViewModel>();
        }
    }
}