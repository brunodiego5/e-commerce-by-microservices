using LoyaltyProgram.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Presentation.Controller
{
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private static readonly IDictionary<int, LoyaltyProgramUser>
            RegisteredUsers = new Dictionary<int, LoyaltyProgramUser>();

        [HttpPost("")]
        public ActionResult<LoyaltyProgramUser> CreateUser(
            [FromBody] LoyaltyProgramUser user)
        {
            if (user == null) return BadRequest();

            var newUser = RegisterUser(user);

            return Created(
                new Uri($"/users/{newUser.Id}", UriKind.Relative),
                newUser);
        }
        
        [HttpPut("{userId:int}")]
        public LoyaltyProgramUser UpdateUser(
            int userId,
            [FromBody] LoyaltyProgramUser user
        ) => RegisteredUsers[userId] = user;

        private LoyaltyProgramUser RegisterUser(LoyaltyProgramUser user)
        {
            // store the new user to a data store
            var userId = RegisteredUsers.Count;

            return RegisteredUsers[userId] = user with { Id = userId};
        }

        [HttpGet("{userId:int}")]
        public ActionResult<LoyaltyProgramUser> GetUser(int userId) =>
            RegisteredUsers.ContainsKey(userId)
                ? (ActionResult<LoyaltyProgramUser>) Ok(RegisteredUsers[userId])
                : NotFound();
    }
}