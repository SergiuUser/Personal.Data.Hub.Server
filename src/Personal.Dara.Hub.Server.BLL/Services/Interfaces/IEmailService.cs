namespace Personal.Dara.Hub.Server.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string email);
    }
}
