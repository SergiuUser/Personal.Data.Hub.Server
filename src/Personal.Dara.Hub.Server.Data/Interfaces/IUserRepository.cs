using Personal.Dara.Hub.Server.Data.Repositories;
using Personal.Dara.Hub.Server.Models.Models.Database;

namespace Personal.Dara.Hub.Server.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<bool> DoesEmailAndUsernameExists(string email, string username);
    }
}
