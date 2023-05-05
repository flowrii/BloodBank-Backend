namespace BloodAPI.Notifications.Services
{
    public interface INotificationServiceFactory
    {
        INotificationService Create(string type);
    }
}
