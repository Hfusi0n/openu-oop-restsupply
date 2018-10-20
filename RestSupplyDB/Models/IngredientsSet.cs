namespace RestSupplyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IngredientsSet")]
    public partial class IngredientsSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IngredientsSet()
        {
            KitchenIngredientsSet = new HashSet<KitchenIngredientsSet>();
            MenuIngredientsSet = new HashSet<MenuIngredientsSet>();
            IngredientListOrdersSet = new HashSet<IngredientListOrdersSet>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Unit { get; set; }

        public int SupplierId { get; set; }

        public double PricePerUnit { get; set; }

        public virtual SuppliersSet SuppliersSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KitchenIngredientsSet> KitchenIngredientsSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuIngredientsSet> MenuIngredientsSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredientListOrdersSet> IngredientListOrdersSet { get; set; }

    }
}
