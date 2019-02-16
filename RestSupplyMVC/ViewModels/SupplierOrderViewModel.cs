using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace RestSupplyMVC.ViewModels
{

    public class SupplierOrderViewModel : CreateSupplierViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<SupplierOrderIngredientsViewModel> SupplierOrderIngredientsList { get; set; }

    }


    public class SupplierOrderIngredientsViewModel : IngredientViewModel
    {
        // This class has many props same as IngredientViewModel - consider using inheritance 
        public int OrderIngredientId { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
    }


}