using RestSupplyDB;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestSupplyDbContext _context;
        public ISupplierRepository Suppliers { get; private set; }
        public IIngredientRepository Ingredients { get; private set; }
        public IMenuItemsRepository MenuItems { get; private set; }
        public ISupplierOrderRepository SupplierOrders { get; set; }
        public IAccountRepository Users { get; set; }

        public UnitOfWork(RestSupplyDbContext context)
        {
            _context = context;
            Suppliers = new SupplierRepository(context);
            Ingredients = new IngredientRepository(context);
            MenuItems = new MenuItemsRepository(context);
            SupplierOrders = new SupplierOrderRepository(context);
            Users = new AccountRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}