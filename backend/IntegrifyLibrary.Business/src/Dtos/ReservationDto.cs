using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public class ReservationDto
{
    public Guid ReservationId { get; init; }
    public Guid BookId { get; set; }
    public DateOnly CreatedAt { get; init; }
    public Guid UserId { get; set; }
}

public class CreateReservationDto
{
    public Guid BookId { get; init; }
}