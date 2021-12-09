using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Presentation.Controller
{
    [Route("/events")]
    public class EventFeedController : ControllerBase
    {
        private readonly IEventStore _eventStore;

        public EventFeedController(IEventStore eventStore) => _eventStore = eventStore;
        

        [HttpGet("")]
        public Event[] Get(
            [FromQuery] long start, 
            [FromQuery] long end = long.MaxValue) =>
                this._eventStore.GetEvents(start, end).ToArray();

    }
}