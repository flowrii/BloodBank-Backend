using BloodAPI.Email;
using BloodAPI.Email.EmailService;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using NuGet.Protocol.Plugins;

namespace BloodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService) {
            _mailService = mailService;
        }

        [HttpPost]
        public IActionResult SendEmail(MailDto request)
        {
            _mailService.SendEmail(request);

            return Ok();
            
        }
    }
}
