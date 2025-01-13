using Personal.Dara.Hub.Server.BLL.Services.Interfaces;

namespace Personal.Dara.Hub.Server.BLL.Services
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmailAsync(string email)
        {
            // TODO: Send generic email
            throw new NotImplementedException();
        }
    }
}
