using SpecialOffers.Domain.Entities;
using SpecialOffers.Domain.Interfaces;

namespace SpecialOffers.Infrastructure.Stores
{
    public class OfferStore : IOfferStore
    {
        private static int currentId = 0;
        private static readonly IDictionary<int, OfferEntity> Offers = new Dictionary<int, OfferEntity>();
        public OfferEntity Create(OfferEntity offer)
        {
            var id = Interlocked.Increment(ref currentId);

            var newOffer = offer with { Id = id};
            return Offers[id] = newOffer;
        }

        public void Delete(int id)
        {
            Offers.Remove(id);
        }

        public OfferEntity Get(int id)
        {
            return Offers[id];
        }

        public bool HasOfferById(int id)
        {
            return Offers.ContainsKey(id);
        }

        public OfferEntity Update(int id, OfferEntity offer)
        {
            return Offers[id] = offer;
        }

        
    }
}