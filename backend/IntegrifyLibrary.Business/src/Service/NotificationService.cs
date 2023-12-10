using IntegrifyLibrary.Domain;
using AutoMapper;

namespace IntegrifyLibrary.Business;

public class NotificationService : BaseService<Notification, NotificationDto, NotificationDto, NotificationDto>, INotificationService
{
    private readonly INotificationRepo _notificationRepo;
    private readonly IMapper _mapper;

    public NotificationService(INotificationRepo notificationRepo, IMapper mapper) : base(notificationRepo, mapper)
    {
        _notificationRepo = notificationRepo;
        _mapper = mapper;
    }

    public async Task<List<NotificationDto>> GetOwnNotifications(Guid userId)
    {
        QueryOptions queryOptions = new QueryOptions();
        var notifications = await _notificationRepo.GetAll(queryOptions);
        List<NotificationDto> ownNotifications = new List<NotificationDto>();
        foreach (var notification in notifications)
        {
            if (notification.UserId == userId)
            {
                ownNotifications.Add(_mapper.Map<NotificationDto>(notification));
            }
        }
        return ownNotifications;
    }
}