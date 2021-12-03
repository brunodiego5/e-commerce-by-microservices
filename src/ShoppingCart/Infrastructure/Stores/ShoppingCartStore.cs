using ShoppingCart.Domain;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Infrastructure.Store
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        private static readonly Dictionary<int, ShoppingCartEntity>
            Database = new Dictionary<int, ShoppingCartEntity>();
        
        public ShoppingCartEntity Get(int userId) =>
            Database.ContainsKey(userId)
            ? Database[userId]
            : new ShoppingCartEntity(userId);
        

        public void Save(ShoppingCartEntity shoppingCart)
        {
            Database[shoppingCart.UserId] = shoppingCart;
        }
    }
}