using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, 
            long lastEventSequenceNumber);

        void Raise(string eventName, object content);
    }
}