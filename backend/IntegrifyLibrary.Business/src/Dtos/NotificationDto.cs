namespace IntegrifyLibrary.Business;

public class NotificationDto
{
    public Guid NotificationId { get; init; }
    public string NotificationMessage { get; set; }
    public string NotificationType { get; set; }
    public Guid UserId { get; set; }
    public Dictionary<string, string> NotificationData { get; set; } = new();
}