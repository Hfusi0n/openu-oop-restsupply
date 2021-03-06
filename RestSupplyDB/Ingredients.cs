//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestSupplyDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingredients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingredients()
        {
            this.OrderIngredients = new HashSet<OrderIngredients>();
            this.MenuIngredients = new HashSet<MenuIngredients>();
            this.KitchenIngredients = new HashSet<KitchenIngredients>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int SupplierId { get; set; }
        public double PricePerUnit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderIngredients> OrderIngredients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuIngredients> MenuIngredients { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KitchenIngredients> KitchenIngredients { get; set; }
    }
}
