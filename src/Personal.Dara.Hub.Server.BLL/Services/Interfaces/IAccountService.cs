using Personal.Dara.Hub.Server.Models;
using Personal.Dara.Hub.Server.Models.Data_transfer_object;

namespace Personal.Dara.Hub.Server.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<(bool loginSucced, string token)> Login(LoginDTO entity);
        public Task<bool> Register(RegisterUserDTO entity);
    }
}
