namespace RestSupplyDB
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RestSupplyDB.Models.AppUser;
    using RestSupplyDB.Models.Customer;
    using RestSupplyDB.Models.Ingredient;
    using RestSupplyDB.Models.Kitchen;
    using RestSupplyDB.Models.Menu;
    using RestSupplyDB.Models.Supplier;

    public partial class RestSupplyDbContext : IdentityDbContext<AppUser, AppRole, string, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public RestSupplyDbContext()
            : base("name=RestSupplyDBModel")
        {
        }
        public static RestSupplyDbContext Create()
        {
            return new RestSupplyDbContext();
        }

        public virtual DbSet<Ingredients> IngredientsSet { get; set; }
        public virtual DbSet<KitchenIngredients> KitchenIngredientsSet { get; set; }
        public virtual DbSet<Kitchens> KitchensSet { get; set; }
        public virtual DbSet<MenuItemIngredients> MenuIngredientsSet { get; set; }
        public virtual DbSet<MenuItems> MenuItemsSet { get; set; }
        public virtual DbSet<SupplierOrderDetails> IngredientListOrdersSet { get; set; }
        public virtual DbSet<SupplierOrders> IngredientOrdersSet { get; set; }
        public virtual DbSet<CustomerOrders> CustomerOrdersSet { get; set; }
        public virtual DbSet<CustomerDetailOrders> CustomerDetailOrdersSet { get; set; }
        public virtual DbSet<Supplier> SuppliersSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("dbo.Users");
            modelBuilder.Entity<AppRole>().ToTable("dbo.Roles");
            modelBuilder.Entity<AppUserClaim>().ToTable("dbo.UserClaims");
            modelBuilder.Entity<AppUserLogin>().ToTable("dbo.UserLogins");
            modelBuilder.Entity<AppUserRole>().ToTable("dbo.UserRoles");

            modelBuilder.Entity<AppUser>().Property(r => r.Id);
            modelBuilder.Entity<AppUserClaim>().Property(r => r.Id);
            modelBuilder.Entity<AppRole>().Property(r => r.Id);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.UserKitchens)
                .WithRequired(e => e.AppUser)
                .HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Ingredients>()
                .HasMany(e => e.KitchenIngredients)
                .WithRequired(e => e.IngredientsSet)
                .HasForeignKey(e => e.IngredientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ingredients>()
                .HasMany(e => e.MenuIngredients)
                .WithRequired(e => e.IngredientsSet)
                .HasForeignKey(e => e.IngredientId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Ingredients>()
                .HasMany(e => e.IngredientListOrders)
                .WithRequired(e => e.IngredientsSet)
                .HasForeignKey(e => e.IngredientId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Ingredients>()
                .HasMany(e => e.SuppliersIngredients)
                .WithRequired(e => e.Ingredient)
                .HasForeignKey(e => e.IngredientId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Kitchens>()
                .HasMany(e => e.KitchenIngredientsSet)
                .WithRequired(e => e.KitchensSet)
                .HasForeignKey(e => e.KitchenId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Kitchens>()
                .HasMany(e => e.CustomerOrdersSet)
                .WithRequired(e => e.Kitchens)
                .HasForeignKey(e => e.KitchenId);

            modelBuilder.Entity<Kitchens>()
                .HasMany(e => e.KitchenUsers)
                .WithRequired(e => e.Kitchen)
                .HasForeignKey(e => e.KitchenId);

            modelBuilder.Entity<Kitchens>()
                .HasMany(e => e.SupplierOrders)
                .WithRequired(e => e.Kitchen)
                .HasForeignKey(e => e.KitchenId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItems>()
                .HasMany(e => e.MenuIngredientsSet)
                .WithRequired(e => e.MenuItemsSet)
                .HasForeignKey(e => e.MenuItemId).WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItems>()
                .HasMany(e => e.CustomerDetailOrdersSet)
                .WithRequired(e => e.MenuItems)
                .HasForeignKey(e => e.MenuItemId).WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierOrders>()
                .HasMany(e => e.SupplierOrderDetails)
                .WithRequired(e => e.OrdersSet)
                .HasForeignKey(e => e.OrderId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.SuppliersIngredients)
                .WithRequired(e => e.Supplier)
                .HasForeignKey(e => e.SupplierId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.IngredientOrdersSet)
                .WithRequired(e => e.SupplierSet)
                .HasForeignKey(e => e.SupplierId).WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerOrders>()
                .HasMany(e => e.CustomerDetailOrders)
                .WithRequired(e => e.CustomerOrder)
                .HasForeignKey(e => e.OrderId).WillCascadeOnDelete(false);
        }
    }
}
