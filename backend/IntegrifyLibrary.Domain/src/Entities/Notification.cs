namespace IntegrifyLibrary.Domain; 

public class Notification {
    public Guid NotificationId { get; init; }
    public string NotificationMessage { get; set; }
    public string NotificationType { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly ModifiedAt { get; set; } = DateOnly.MinValue;
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Dictionary<string, string> NotificationData { get; set; } = new();
}
