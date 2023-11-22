namespace IntegrifyLibrary.Business;

public interface IReservationService : IBaseService<CreateReservationDto, ReservationDto, ReservationDto>
{
    Task<ReservationDto> CreateReservation(CreateReservationDto dto, Guid userId);
    Task<List<ReservationDto>> GetOwnReservations(Guid userId);
}