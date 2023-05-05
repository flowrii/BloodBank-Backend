using BloodAPI.Notifications.Services;

namespace BloodAPI.Notifications
{
    public class NotificationFactory : INotificationServiceFactory
    {
        private readonly IConfiguration _config;

        public NotificationFactory(IConfiguration config)
        {
            _config = config;
        }

        public INotificationService Create(string type)
        {
            switch (type.ToLower())
            {
                case "email":
                    return new MailService(_config);
                case "sms":
                    return new SMSService(_config);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
