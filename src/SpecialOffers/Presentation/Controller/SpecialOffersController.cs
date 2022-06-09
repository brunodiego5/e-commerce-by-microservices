using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain.Entities;
using SpecialOffers.Domain.Interfaces;

namespace SpecialOffers.Presentation.Controller
{
  [Route("/offers")]
  public class SpecialOffersController : ControllerBase
  {
    private readonly IEventStore _eventStore;
    private readonly IOfferStore _offerStore;

    public SpecialOffersController(IEventStore eventStore, IOfferStore offerStore) 
    {
      _eventStore = eventStore;

      _offerStore = offerStore;
    }

    [HttpGet("{id:int}")]
    public ActionResult<OfferEntity> GetOffer(int id) =>
      _offerStore.HasOfferById(id)
        ? (ActionResult<OfferEntity>) Ok(_offerStore.Get(id))
        : NotFound();
    
    [HttpPost("")]
    public ActionResult<OfferEntity> CreateOffer([FromBody] OfferEntity offer)
    {
      var newOffer = NewOffer(offer);
      return Created(new Uri($"/offers/{newOffer.Id}", UriKind.Relative), newOffer);
    }

    [HttpPut("{id:int}")]
    public OfferEntity UpdateOffer(int id, [FromBody] OfferEntity offer)
    {
      var updatedOffer = _offerStore.Update(id, offer);
      _eventStore.Raise("SpecialOfferUpdated", updatedOffer);
      return updatedOffer;
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteOffer(int id)
    {
      var deletedOffer = _offerStore.Get(id);
      _offerStore.Delete(id);
      _eventStore.Raise("SpecialOfferRemoved", deletedOffer);
      return Ok();
    }
    

    private OfferEntity NewOffer(OfferEntity offer)
    {
      var newOffer = _offerStore.Create(offer);
      _eventStore.Raise("SpecialOfferCreated", newOffer);
      return newOffer;
    }
  }
}