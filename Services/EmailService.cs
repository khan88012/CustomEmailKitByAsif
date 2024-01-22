using CustomEmailKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.Extensions.Configuration;

namespace CustomEmailKit.Services
{
    public class EmailService : IEmailService
    {
        
            private readonly IConfiguration _config;

            public EmailService(IConfiguration config)
            {
                _config = config;

            }
            public void SendEmail(EmailDto request)
            {

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Emailing:EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("Emailing:EmailHost").Value, Convert.ToInt32(_config.GetSection("Emailing:Port").Value), MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("Emailing:EmailUsername").Value, _config.GetSection("Emailing:EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

            }
        
    }
}
