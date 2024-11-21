using Personal.Dara.Hub.Server.Data.Context;
using Personal.Dara.Hub.Server.Data.Interfaces;
using Personal.Dara.Hub.Server.Models.Models;

namespace Personal.Dara.Hub.Server.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ServerDbContext _context;

        public UserRepository(ServerDbContext context) : base(context)
        {
           _context = context;
        }

        // TODO: Add methods for user

    }
}
