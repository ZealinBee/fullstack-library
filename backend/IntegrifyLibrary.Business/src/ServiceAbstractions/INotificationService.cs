using IntegrifyLibrary.Domain;
namespace IntegrifyLibrary.Business;

public interface INotificationService : IBaseService<NotificationDto, NotificationDto, NotificationDto>
{
    Task<List<NotificationDto>> GetOwnNotifications(Guid userId);
}