namespace IntegrifyLibrary.Domain;

public record Reservation {
    public Guid ReservationId { get; init; }
    public Book Book { get; init; } = new();
    public Guid UserId { get; init; }
    public User User { get; init; }
    public DateOnly CreatedAt { get; init; }
    public DateOnly ModifiedAt { get; init; } = DateOnly.MinValue;
}