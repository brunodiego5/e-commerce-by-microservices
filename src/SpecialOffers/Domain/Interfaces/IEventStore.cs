using SpecialOffers.Domain.Entities;

namespace SpecialOffers.Domain.Interfaces
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, 
            long lastEventSequenceNumber);

        void Raise(string eventName, object content);
    }
}