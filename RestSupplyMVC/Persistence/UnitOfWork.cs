using RestSupplyDB;
using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Persistence
{
    public class UnitOfWork
    {
        private readonly RestSupplyDbContext _context;

        // set is private becuase we want to init the repository only inside the UoW c-tor
        public SupplierRepository Suppliers { get; private set; }
        public IngredientRepository Ingredients { get; private set; }
        public MenuItemsRepository MenuItems { get; private set; }

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