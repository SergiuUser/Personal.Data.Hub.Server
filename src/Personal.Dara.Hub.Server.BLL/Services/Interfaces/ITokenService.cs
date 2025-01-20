using Personal.Dara.Hub.Server.Models.Models.Services;

namespace Personal.Dara.Hub.Server.BLL.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserInfoToGenerateJwtToken login);
    }
}
