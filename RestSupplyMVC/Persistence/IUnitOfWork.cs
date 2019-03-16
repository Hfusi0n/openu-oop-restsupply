using RestSupplyMVC.Repositories;

namespace RestSupplyMVC.Persistence
{
    public interface IUnitOfWork
    {
        ISupplierRepository Suppliers { get; }
        IIngredientRepository Ingredients { get; }
        IMenuItemsRepository MenuItems { get; }
        ISupplierOrderRepository SupplierOrders { get; }
        IKitchenRepository Kitchens { get; }
        IKitchenIngredientRepository KitchenIngredient { get; }
        ICustomerOrderRepository CustomerOrder { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }

        void Complete();
    }
}