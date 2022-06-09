using SpecialOffers.Domain.Entities;

namespace SpecialOffers.Domain.Interfaces
{
    public interface IOfferStore
    {
        OfferEntity Get(int id);
        OfferEntity Create(OfferEntity offer);
        OfferEntity Update(int id, OfferEntity offer);
        void Delete(int id);
        bool HasOfferById(int id);

    }
}