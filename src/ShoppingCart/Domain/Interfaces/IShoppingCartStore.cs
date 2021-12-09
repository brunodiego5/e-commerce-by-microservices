using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IShoppingCartStore
    {
        ShoppingCartEntity Get(int userId);
        void Save(ShoppingCartEntity shoppingCart);
    }
}