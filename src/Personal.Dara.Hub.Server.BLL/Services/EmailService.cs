using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Personal.Dara.Hub.Server.BLL.Services.Interfaces;
using Personal.Dara.Hub.Server.Models.Models.Email;
using Personal.Dara.Hub.Server.Models.Models.Sections;

namespace Personal.Dara.Hub.Server.BLL.Services
{
    public class EmailService : IEmailService
    {

        private readonly string EMAIL_HOST = "EmailConfiguration:Host";
        private readonly string EMAIL_DISPLAYNAME = "EmailConfiguration:DisplayName";
        private readonly string EMAIL_USERNAME= "EmailConfiguration:Username"; 
        private readonly string EMAIL_PORT = "EmailConfiguration:Port"; 

        #region Services and the constructor
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) => _configuration = configuration;
        #endregion

        public Task<bool> SendEmailAsync(EmailBodyModel emailBody)
        {
            try
            {
                // TODO: Idea -- Change this with sections


                var message = new MimeMessage();
                    string? host = _configuration[EMAIL_HOST]; 
                    string? displayName = _configuration[EMAIL_DISPLAYNAME];
                    string? username = _configuration[EMAIL_USERNAME];

                int port = 587;
                if (int.TryParse(_configuration[EMAIL_PORT], out int parsedPort))
                {
                    port = parsedPort;
                }

                message.From.Add(new MailboxAddress(displayName, username));
                message.To.Add(new MailboxAddress(string.Empty, emailBody.Email));
                message.Subject = emailBody.Subject;
                message.Body = new TextPart("plain") { Text = emailBody.Message };

                using (var client = new SmtpClient())
                {
                    client.Connect(host, port, SecureSocketOptions.StartTls);
                    client.Authenticate(host, "mwjcsjaggaqnuhnz");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return Task.FromResult(true);
            } catch 
            {
                throw;
            }

        }
    }
}
