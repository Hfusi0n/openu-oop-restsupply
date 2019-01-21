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

        public UnitOfWork(RestSupplyDbContext context)
        {
            _context = context;
            Suppliers = new SupplierRepository(context);
            Ingredients = new IngredientRepository(context);
            MenuItems = new MenuItemsRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}