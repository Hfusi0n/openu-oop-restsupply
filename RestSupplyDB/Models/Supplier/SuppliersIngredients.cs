using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSupplyDB.Models.Supplier
{
    [Table("SupliersIngredients")]
    class SuppliersIngredients
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }

        public int IngredientId { get; set; }

        public double Price { get; set; }

    }
}
