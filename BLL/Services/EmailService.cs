using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.Interfaces;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smptServer;
        private readonly string _username;
        private readonly string _password;
        private readonly string _yourEmail;

        public EmailService(string smptServer, string username, string password, string yourEmail)
        {
            _smptServer = smptServer;
            _username = username;
            _password = password;
            _yourEmail = yourEmail;
        }
        public async Task SendEmailAsync(string receiverEmail, string subject, string body)
        {
            var client = new SmtpClient(_smptServer)
            {
                Port = 587,
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true,
            };
            client.Send(_yourEmail, receiverEmail, subject, body);
        }
    }
}
