using Personal.Dara.Hub.Server.Models.Helpers;

namespace Personal.Dara.Hub.Server.Models.Models.Services
{
    public class UserInfoToGenerateJwtToken
    {
       public string Email { get; set; }
       public UserRoleHelper Role { get; set; }
    }
}
