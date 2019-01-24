using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSupplyDB.Models.Kitchen
{
    public class KitchenUsers
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int KitchenId { get; set; }

        public virtual AppUser.AppUser AppUser { get; set; }

        public virtual Kitchens Kitchen { get; set; }
    }
}
