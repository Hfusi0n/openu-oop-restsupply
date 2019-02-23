using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace RestSupplyMVC.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<SupplierOrderViewModel> Orders { get; set; }

        /// <summary>
        /// Indicate if the details modal should be displayed in the main view
        /// </summary>
        public bool DisplayDetails { get; set; } = false;

        /// <summary>
        /// Indicate if the Edit modal should be displayed in the main view
        /// </summary>
        public bool DisplayEdit { get; set; } = false;

        /// <summary>
        /// Indicate if the Delete modal should be displayed in the main view
        /// </summary>
        public bool DisplayDelete { get; set; } = false;


        /// <summary>
        /// If there's a need to display a modal for one of the orders 
        /// The order id to display will be set to this variable
        /// </summary>
        public int? ModalOrderId { get; set; } = -1;
    }

    public class SupplierOrderViewModel : CreateSupplierViewModel
    {
        public int OrderId { get; set; }
        public int KitchenId { get; set; }
        public List<KitchenViewModel> UserKitchens { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public IEnumerable<SupplierOrderIngredientsViewModel> SupplierOrderIngredientsList { get; set; }
    }

    public class SupplierOrderIngredientsViewModel : IngredientViewModel
    {
        // This class has many props same as IngredientViewModel - consider using inheritance         
        public int OrderIngredientId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
    }
}