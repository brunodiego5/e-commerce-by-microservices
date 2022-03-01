using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Infrastructure.EventFeed
{
    public class EventStore : IEventStore
    {
        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            //throw new NotImplementedException();
            var list = new List<Event>();
            
            list.Add( 
                new Event(1, DateTimeOffset.UtcNow, "ShoppingCartItemAdded", "content")
            );

            return list;
        }

        public void Raise(string eventName, object content)
        {
            //throw new NotImplementedException();
        }
    }
}