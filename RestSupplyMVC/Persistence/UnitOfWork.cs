using System.Data.Entity.Validation;
using System.Diagnostics;
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
        public IKitchenIngredientRepository KitchenIngredient { get; }
        public ICustomerOrderRepository CustomerOrder { get; }
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }

        public UnitOfWork(RestSupplyDbContext context)
        {
            _context = context;
            Suppliers = new SupplierRepository(context);
            Ingredients = new IngredientRepository(context);
            MenuItems = new MenuItemsRepository(context);
            SupplierOrders = new SupplierOrderRepository(context);
            Kitchens = new KitchenRepository(context);
            KitchenIngredient = new KitchenIngredientRepository(context);
            CustomerOrder = new CustomerOrderRepository(context);
            Users = new UserRepository(context);
            Roles = new RoleRepository(context);
        }

        public void Complete()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}