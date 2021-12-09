using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IProductCatalogClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productIds);
    }
}