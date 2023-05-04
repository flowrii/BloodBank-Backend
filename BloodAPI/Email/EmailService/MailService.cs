﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace BloodAPI.Email.EmailService
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _config;
        public MailService(IConfiguration config) 
        {
            _config = config;
        }
        public void SendEmail(MailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("lenore.smitham@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
