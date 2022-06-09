/**
This class could be a commamnd, 
but it's a simpler example, 
a command abstraction (CQRS) 
**/
namespace LoyaltyProgram.Domain
{
    public record LoyaltyProgramUser(
        int Id,
        string Name,
        int LoyaltyPoints,
        LoyaltyProgramSettings Settings);

    public record LoyaltyProgramSettings()
    {
        public LoyaltyProgramSettings(string[] interests) : this()
        {
            this.Interests = interests;
        }

        public string[] Interests { get; init; } = Array.Empty<string>();
    }
}