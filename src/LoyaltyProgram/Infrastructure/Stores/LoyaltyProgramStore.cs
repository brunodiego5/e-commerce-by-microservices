using LoyaltyProgram.Domain;
using LoyaltyProgram.Domain.Interfaces;

namespace LoyaltyProgram.Infrastructure.Stores
{
    public class LoyaltyProgramStore : ILoyaltyProgramStore
    {
        private static int currentId = 0;
        private static readonly IDictionary<int, LoyaltyProgramUser> Users = 
            new Dictionary<int, LoyaltyProgramUser>();
        public LoyaltyProgramUser Create(LoyaltyProgramUser user)
        {
            var id = Interlocked.Increment(ref currentId);

            var newUser = user with { Id = id};

            return Users[id] = newUser;
        }

        public LoyaltyProgramUser Get(int id)
        {
            return Users[id];
        }

        public LoyaltyProgramUser Update(int id, LoyaltyProgramUser user)
        {
            return Users[id] = user;
        }

        public bool HasUserById(int id)
        {
            return Users.ContainsKey(id);
        }
    }
}