using ShoppingCart.Domain;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Infrastructure.Services
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productIds)
        {
            throw new NotImplementedException();
        }
    }
}