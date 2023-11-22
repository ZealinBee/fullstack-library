using IntegrifyLibrary.Domain;
using AutoMapper;

namespace IntegrifyLibrary.Business;

public class ReservationService : BaseService<Reservation, ReservationDto, ReservationDto, ReservationDto>, IReservationService {
    private readonly IReservationRepo _reservationRepo;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepo reservationRepo, IMapper mapper) : base(reservationRepo, mapper) { 
        _reservationRepo = reservationRepo;
        _mapper = mapper;
    }
}