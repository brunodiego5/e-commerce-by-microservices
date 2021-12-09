namespace ShoppingCart.Domain.Entities
{
    public record Event
    (
        long SequenceNumber,
        DateTimeOffset OccurredAt,
        string Name,
        object Content
    );
}