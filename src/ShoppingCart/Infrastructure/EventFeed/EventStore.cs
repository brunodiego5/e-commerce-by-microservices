using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Infrastructure.EventFeed
{
    public class EventStore : IEventStore
    {
        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            throw new NotImplementedException();
        }

        public void Raise(string eventName, object content)
        {
            throw new NotImplementedException();
        }
    }
}