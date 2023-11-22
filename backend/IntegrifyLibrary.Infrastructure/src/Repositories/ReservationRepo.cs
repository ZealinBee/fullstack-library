using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Controllers;
using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class ReservationRepo : BaseRepo<Reservation>, IReservationRepo
{
    private readonly DatabaseContext _context;
    public ReservationRepo(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}