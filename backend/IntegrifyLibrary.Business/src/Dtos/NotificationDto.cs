namespace IntegrifyLibrary.Business;

public class NotificationDto
{
    public string NotificationMessage { get; set; }
    public string NotificationType { get; set; }
    public Guid UserId { get; set; }
}