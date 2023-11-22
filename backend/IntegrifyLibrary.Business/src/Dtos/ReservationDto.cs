using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public class ReservationDto {
    public Guid ReservationId { get; init; }
    public BookDto Book { get; set; }
    public DateOnly CreatedAt { get; init; }
}

public class CreateReservationDto {
    public Guid BookId { get; init; }
}