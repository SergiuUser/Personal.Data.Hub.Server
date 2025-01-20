using Personal.Dara.Hub.Server.Models.Models.Email;

namespace Personal.Dara.Hub.Server.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(EmailBodyModel emailBody);
    }
}
