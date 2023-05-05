using BloodAPI.Notifications.DTOs;
using BloodAPI.Notifications.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BloodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationServiceFactory _factory;

        public NotificationsController(INotificationServiceFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("{type}")]
        public IActionResult SendNotification(string type, [FromBody] NotificationDto notificationDto)
        {
            try
            {
                var notificationService = _factory.Create(type);
                notificationService.SendNotification(notificationDto);

                //reminder scheduler
                var jobId = $"SendReminder_{notificationDto.AppointmentId}";
                var reminderDate = notificationDto.Date.AddDays(-1);
                BackgroundJob.Schedule(() => SendReminder(type, notificationDto), reminderDate);


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{type}/reminder")]
        public IActionResult SendReminder(string type, [FromBody] NotificationDto notificationDto)
        {
            var notificationService = _factory.Create(type);

            notificationDto.Body += "\n\nThis is a reminder for your appointment tomorrow.";

            notificationService.SendNotification(notificationDto);

            return Ok();
        }

    }
}
