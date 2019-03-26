using System.Collections.Generic;
using Microsoft.Ajax.Utilities;

namespace RestSupplyMVC.ViewModels
{
    public class KitchenIndexViewModel
    {
        /* In Select Kitchen mode - all actions will be hidden, except kitchen name
         When kitchen is selected, user will be redirected to the index action
         of the controller in ControllerRedirect, with the selected kitchenId */
        public bool IsSelectKitchen
        {
            get
            {
                if (ControllerRedirect.IsNullOrWhiteSpace())
                    return false;
                return true;
            }
        }

        public CreateKitchenViewModel KitchenToCreate { get; set; }
        public List<KitchenViewModel> KitchensList { get; set; }

        // Controller name to redirect to when user selects kitchen
        public string ControllerRedirect { get; set; }
    }
}