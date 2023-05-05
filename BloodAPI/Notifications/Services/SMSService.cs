using BloodAPI.Notifications.DTOs;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BloodAPI.Notifications.Services
{
    public class SMSService : INotificationService
    {
        private readonly IConfiguration _config;
        public SMSService(IConfiguration config)
        {
            _config = config;
        }
        public void SendNotification(NotificationDto request)
        {
            string accountSid = _config.GetSection("TWILIO_ACCOUNT_SID").Value;
            string authToken = _config.GetSection("TWILIO_AUTH_TOKEN").Value;

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: request.Body,
                from: new Twilio.Types.PhoneNumber(request.FromSubject),
                //from: new Twilio.Types.PhoneNumber("+13203027670"),
                to: new Twilio.Types.PhoneNumber(request.To)
            //to: new Twilio.Types.PhoneNumber("+40735539291")
            );
        }
    }
}
