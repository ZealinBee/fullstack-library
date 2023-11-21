using IntegrifyLibrary.Domain;
using AutoMapper;

namespace IntegrifyLibrary.Business;

public class NotificationService : BaseService<Notification, NotificationDto,NotificationDto, NotificationDto>, INotificationService
{
    private readonly INotificationRepo _notificationRepo;
    private readonly IMapper _mapper;

    public NotificationService(INotificationRepo notificationRepo, IMapper mapper) : base(notificationRepo, mapper)
    {
        _notificationRepo = notificationRepo;
        _mapper = mapper;
    }
}