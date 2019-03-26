using System.Collections.Generic;
using System.Linq;

namespace RestSupplyMVC.ViewModels
{
    public class CreateKitchenViewModel : KitchenViewModel
    {
        public List<UserViewModel> AllUsersList { get; set; }
        
        // Only users that are not associated with the current kitchen
        public List<UserViewModel> NotAssociatedUsers
        {
            get { return AllUsersList.Where(i => KitchenUsersList.All(si => si.Id != i.Id)).ToList(); }

        }
    }
}