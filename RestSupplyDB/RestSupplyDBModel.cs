namespace RestSupplyDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RestSupplyDB.Models;

    public partial class RestSupplyDBModel : IdentityDbContext<UsersSet>
    {
        public RestSupplyDBModel()
            : base("name=RestSupplyDBModel")
        {
        }

        public virtual DbSet<IngredientsSet> IngredientsSet { get; set; }
        public virtual DbSet<KitchenIngredientsSet> KitchenIngredientsSet { get; set; }
        public virtual DbSet<KitchensSet> KitchensSet { get; set; }
        public virtual DbSet<MenuIngredientsSet> MenuIngredientsSet { get; set; }
        public virtual DbSet<MenuItemsSet> MenuItemsSet { get; set; }
        public virtual DbSet<IngredientListOrdersSet> IngredientListOrdersSet { get; set; }
        public virtual DbSet<IngredientOrdersSet> IngredientOrdersSet { get; set; }
        public virtual DbSet<CustomerOrdersSet> CustomerOrdersSet { get; set; }
        public virtual DbSet<SuppliersSet> SuppliersSet { get; set; }
        public virtual DbSet<UsersSet> UsersSet { get; set; }
        public virtual DbSet<RolesSet> RolesSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IngredientsSet>()
                .HasMany(e => e.KitchenIngredientsSet)
                .WithRequired(e => e.IngredientsSet)
                .HasForeignKey(e => e.IngredientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IngredientsSet>()
                .HasMany(e => e.MenuIngredientsSet)
                .WithOptional(e => e.IngredientsSet)
                .HasForeignKey(e => e.IngredientId);

            modelBuilder.Entity<IngredientsSet>()
                .HasMany(e => e.IngredientListOrdersSet)
                .WithRequired(e => e.IngredientsSet)
                .HasForeignKey(e => e.IngredientId);

            modelBuilder.Entity<KitchensSet>()
                .HasMany(e => e.KitchenIngredientsSet)
                .WithRequired(e => e.KitchensSet)
                .HasForeignKey(e => e.KitchenId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KitchensSet>()
                .HasMany(e => e.CustomerOrdersSet)
                .WithRequired(e => e.KitchensSet)
                .HasForeignKey(e => e.KitchenId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItemsSet>()
                .HasMany(e => e.MenuIngredientsSet)
                .WithOptional(e => e.MenuItemsSet)
                .HasForeignKey(e => e.MenuItemId);

            modelBuilder.Entity<MenuItemsSet>()
                .HasMany(e => e.CustomerOrdersSet)
                .WithRequired(e => e.MenuItemsSet)
                .HasForeignKey(e => e.MenuItemId);

            modelBuilder.Entity<IngredientOrdersSet>()
                .HasMany(e => e.IngredientListOrdersSet)
                .WithRequired(e => e.OrdersSet)
                .HasForeignKey(e => e.OrderId);

            modelBuilder.Entity<SuppliersSet>()
                .HasMany(e => e.IngredientsSet)
                .WithRequired(e => e.SuppliersSet)
                .HasForeignKey(e => e.SupplierId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SuppliersSet>()
                .HasMany(e => e.IngredientOrdersSet)
                .WithRequired(e => e.SuppliersSet)
                .HasForeignKey(e => e.SupplierId);
        }
    }
}
