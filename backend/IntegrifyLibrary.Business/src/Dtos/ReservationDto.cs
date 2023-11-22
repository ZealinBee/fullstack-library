using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public class ReservationDto {
    public Guid ReservationId { get; init; }
    public Book Book { get; init; } = new();
    public Guid UserId { get; init; }
    public GetUserDto User { get; init; }
    public DateOnly CreatedAt { get; init; }
}