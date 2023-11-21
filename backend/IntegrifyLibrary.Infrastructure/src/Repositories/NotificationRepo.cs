using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Controllers;
using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class NotificationRepo : BaseRepo<Notification>, INotificationRepo
{
    private readonly DatabaseContext _context;
    public NotificationRepo(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}