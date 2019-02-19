using RestSupplyDB;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestSupplyDbContext _context;
        public ISupplierRepository Suppliers { get; }
        public IIngredientRepository Ingredients { get; }
        public IMenuItemsRepository MenuItems { get; }
        public ISupplierOrderRepository SupplierOrders { get; }
        public IKitchenRepository Kitchens { get; }
        public IAccountRepository Account { get; set; }

        public UnitOfWork(RestSupplyDbContext context)
        {
            _context = context;
            Suppliers = new SupplierRepository(context);
            Ingredients = new IngredientRepository(context);
            MenuItems = new MenuItemsRepository(context);
            SupplierOrders = new SupplierOrderRepository(context);
            Account = new AccountRepository(context);
            Kitchens = new KitchenRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}