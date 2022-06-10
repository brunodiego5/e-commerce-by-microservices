namespace LoyaltyProgram.Domain.Interfaces
{
    public interface ILoyaltyProgramStore
    {
        LoyaltyProgramUser Get(int id);

        LoyaltyProgramUser Create(LoyaltyProgramUser user);

        LoyaltyProgramUser Update(int id, LoyaltyProgramUser user);

        bool HasUserById(int id);

    }
}