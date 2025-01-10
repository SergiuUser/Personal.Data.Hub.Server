using Microsoft.EntityFrameworkCore;
using Personal.Dara.Hub.Server.Data.Context;
using Personal.Dara.Hub.Server.Data.Interfaces;
using Personal.Dara.Hub.Server.Models.Models;

namespace Personal.Dara.Hub.Server.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ServerDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(ServerDbContext context) : base(context)
        {
           _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<bool> DoesEmailAndUsernameExists(string email, string username)
        {
            if (await _dbSet.AnyAsync(u => u.Username == username || u.Email == email))
                return true;

            return false;
        }
    }
}
