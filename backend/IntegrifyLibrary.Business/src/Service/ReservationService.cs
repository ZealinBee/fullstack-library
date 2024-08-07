using IntegrifyLibrary.Domain;
using AutoMapper;

namespace IntegrifyLibrary.Business;

public class ReservationService : BaseService<Reservation, CreateReservationDto, ReservationDto, ReservationDto>, IReservationService
{
    private readonly IReservationRepo _reservationRepo;
    private readonly IMapper _mapper;
    private readonly IBookRepo _bookRepo;

    public ReservationService(IReservationRepo reservationRepo, IMapper mapper, IBookRepo bookRepo) : base(reservationRepo, mapper)
    {
        _reservationRepo = reservationRepo;
        _mapper = mapper;
        _bookRepo = bookRepo;
    }

    public async Task<ReservationDto> CreateReservation(CreateReservationDto dto, Guid userId)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        var reservation = _mapper.Map<Reservation>(dto);
        var book = await _bookRepo.GetOne(dto.BookId);
        if (book.Quantity > 0) throw new CustomException().BadRequestException("Book is available, no need to reserve");
        reservation.Book = book;
        reservation.UserId = userId;
        var createdReservation = await _reservationRepo.CreateOne(reservation);
        return _mapper.Map<ReservationDto>(createdReservation);
    }

    public async Task<List<ReservationDto>> GetOwnReservations(Guid userId)
    {
        QueryOptions queryOptions = new QueryOptions();
        var reservations = await _reservationRepo.GetAll(queryOptions);
        List<ReservationDto> ownReservations = new List<ReservationDto>();
        foreach (var reservation in reservations)
        {
            if (reservation.UserId == userId)
            {
                ownReservations.Add(_mapper.Map<ReservationDto>(reservation));
            }
        }
        return ownReservations;
    }


}