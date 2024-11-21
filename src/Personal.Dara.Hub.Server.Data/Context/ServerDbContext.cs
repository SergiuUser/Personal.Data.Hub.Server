using Microsoft.EntityFrameworkCore;
using Personal.Dara.Hub.Server.Models.Models;

namespace Personal.Dara.Hub.Server.Data.Context
{
    public class ServerDbContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }

        public ServerDbContext (DbContextOptions<ServerDbContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
