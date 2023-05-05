using BloodAPI.Notifications.DTOs;

namespace BloodAPI.Notifications.Services
{
    public interface INotificationService
    {
        void SendNotification(NotificationDto request);
    }
}
