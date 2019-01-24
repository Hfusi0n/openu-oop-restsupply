using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSupplyDB.Models.Ingredient;

namespace RestSupplyDB.Models.Supplier
{
    [Table("SupliersIngredients")]
    public class SuppliersIngredients
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }

        public int IngredientId { get; set; }

        public double Price { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Ingredients Ingredient { get; set; }
    }
}
