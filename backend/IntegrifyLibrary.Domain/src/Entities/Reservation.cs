namespace IntegrifyLibrary.Domain;

public record Reservation {
    public Guid ReservationId { get; init; }
    public Book Book { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; init; }
    public DateOnly CreatedAt { get; init;}
    public DateOnly ModifiedAt { get; set; } = DateOnly.MinValue;
}