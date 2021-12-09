namespace ShoppingCart.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ShoppingCart.Domain.Interfaces;

    public class ShoppingCartEntity
    {
        private readonly HashSet<ShoppingCartItem> items = new();
        public int UserId { get; }
        public IEnumerable<ShoppingCartItem> Items => this.items;
        public ShoppingCartEntity(int userId) => this.UserId = userId;

        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventStore eventStore)
        {
            foreach (var item in shoppingCartItems)
            {
                if (this.items.Add(item))
                    eventStore.Raise("ShoppingCartItemAdded", new { UserId, item });
            }
        }

        public void RemoveItems(int[] productCatalogueIds, IEventStore eventStore)
        {
            foreach (int id in productCatalogueIds)
            {
                var item = this.items.FirstOrDefault(i => i.ProductCatalogueId == id);
                
                if (item == null) throw new ArgumentNullException(nameof(item));

                if (this.items.Remove(item))
                    eventStore.Raise("ShoppingCartItemRemoved", new { UserId, item });
            }
            this.items.RemoveWhere(i => productCatalogueIds.Contains(
                i.ProductCatalogueId));
        }
        
    }

    public record ShoppingCartItem(
        int ProductCatalogueId,
        string ProductName,
        string Description,
        Money Price)
    {
        public virtual bool Equals(ShoppingCartItem? obj) =>
            obj != null && this.ProductCatalogueId.Equals(obj.ProductCatalogueId);

        public override int GetHashCode() =>
            this.ProductCatalogueId.GetHashCode();
    }
    public sealed record Money(string Currency, decimal Amount);    
}