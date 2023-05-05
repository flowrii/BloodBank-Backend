namespace BloodAPI.Notifications.DTOs
{
    public class NotificationDto
    {
        public string To { get; set; } = string.Empty;
        public string FromSubject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string NotificationType { get; set; } = string.Empty;
    }
}
