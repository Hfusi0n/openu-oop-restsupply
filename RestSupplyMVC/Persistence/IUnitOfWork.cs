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
        IAccountRepository Account { get; }
        void Complete();
    }
}