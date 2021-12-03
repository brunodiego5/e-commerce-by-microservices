using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Controller
{
    [Route("/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore _shoppingCartStore;
        private readonly IProductCatalogClient _productCatalogClient;
        private readonly IEventStore _eventStore;

        public ShoppingCartController(
            IShoppingCartStore shoppingCartStore, 
            IProductCatalogClient productCatalogClient, 
            IEventStore eventStore)
        {
            this._shoppingCartStore = shoppingCartStore;
            this._productCatalogClient = productCatalogClient;
            this._eventStore = eventStore;
        }

        [HttpGet("{userId:int}")]
        public ShoppingCartEntity Get(int userId) =>
            this._shoppingCartStore.Get(userId);

        [HttpPost("{userId:int}/items")]
        public async Task<ShoppingCartEntity> Post (
            int userId, 
            [FromBody] int[] productIds)
        {
            var shoppingCart = _shoppingCartStore.Get(userId);
            var shoppingCartItems = 
                await this._productCatalogClient.GetShoppingCartItems(productIds);

            shoppingCart.AddItems(shoppingCartItems, _eventStore);

            _shoppingCartStore.Save(shoppingCart);

            return shoppingCart;   
        }       
    }
}