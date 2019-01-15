using System.Web.Mvc;

namespace RestSupplyMVC.ViewModels
{
    public class CustomerOrderViewModel
    {
        public SelectList MenuItems { get; set; }
        public string SelectedMenuItem { get; set; }
    }
}