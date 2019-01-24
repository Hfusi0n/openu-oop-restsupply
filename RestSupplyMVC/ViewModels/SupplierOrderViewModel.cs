using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace RestSupplyMVC.ViewModels
{
    public class SupplierOrderViewModel
    {
        // This class has many props same as SupplierViewModel - consider using inheritance 

        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        [Display(Name = "שם ספק")] public string SupplierName { get; set; }
        [Display(Name = "כתובת")] public string SupplierAddress { get; set; }
        [Display(Name = "טלפון")] public string SupplierPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<SupplierOrderIngredientsViewModel> OrderIngredientsList { get; set; }

    }

    public class SupplierOrderIngredientsViewModel 
    {
        // This class has many props same as IngredientViewModel - consider using inheritance 
        public int OrderId { get; set; }
        [Display(Name = "שם חומר גלם")]
        public string IngredientName { get; set; }
        public int IngredientId { get; set; }
        [Display(Name = "יחידת מידה")]
        public string Unit { get; set; }
        public double Amount { get; set; }
    }

    public class CreateSupplierOrderViewModel : SupplierOrderViewModel
    {
        public IEnumerable<SupplierViewModel> AllSuppliers { get; set; }
        public IEnumerable<IngredientViewModel> SupplierIngredients { get; set; }

    }

    public class SupplierOrderIndexViewModel
    {
        public CreateSupplierOrderViewModel CreateSupplierOrderViewModel { get; set; }
        public IEnumerable<SupplierOrderViewModel> SupplierOrdersList { get; set; }
    }
}