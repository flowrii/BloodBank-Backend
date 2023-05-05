using BloodAPI.Notifications.DTOs;
using BloodAPI.Notifications.Services;
using Microsoft.AspNetCore.Http;
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
