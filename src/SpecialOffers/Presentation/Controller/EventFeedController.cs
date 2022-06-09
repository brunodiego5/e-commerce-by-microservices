using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain.Entities;
using SpecialOffers.Domain.Interfaces;

namespace SpecialOffers.Presentation.Controller
{
    [Route("/events")]
    public class EventFeedController : ControllerBase
    {
        private readonly IEventStore _eventStore;

        public EventFeedController(IEventStore eventStore) => _eventStore = eventStore;
        

        [HttpGet("")]
        public ActionResult<Event[]> GetEvents(
            [FromQuery] long start, 
            [FromQuery] long end = long.MaxValue)
        {
            if (start < 0 || end < start) return BadRequest();

            return _eventStore.GetEvents(start, end).ToArray();
        }
    }
}