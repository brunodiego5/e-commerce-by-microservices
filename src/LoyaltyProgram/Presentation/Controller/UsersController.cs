using LoyaltyProgram.Domain;
using LoyaltyProgram.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Presentation.Controller
{
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILoyaltyProgramStore _loyaltyProgramStore;

        public UsersController(ILoyaltyProgramStore loyaltyProgramStore)
        {
            _loyaltyProgramStore = loyaltyProgramStore;
        }

        [HttpPost("")]
        public ActionResult<LoyaltyProgramUser> CreateUser(
            [FromBody] LoyaltyProgramUser user)
        {
            if (user == null) return BadRequest();

            var newUser = _loyaltyProgramStore.Create(user);

            return Created(
                new Uri($"/users/{newUser.Id}", UriKind.Relative),
                newUser);
        }
        
        [HttpPut("{userId:int}")]
        public LoyaltyProgramUser UpdateUser(
            int userId,
            [FromBody] LoyaltyProgramUser user
        ) => _loyaltyProgramStore.Update(userId, user);

        [HttpGet("{userId:int}")]
        public ActionResult<LoyaltyProgramUser> GetUser(int userId) =>
            _loyaltyProgramStore.HasUserById(userId)
                ? (ActionResult<LoyaltyProgramUser>) Ok(_loyaltyProgramStore.Get(userId))
                : NotFound();
    }
}