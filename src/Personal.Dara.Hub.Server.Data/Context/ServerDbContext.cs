using Microsoft.EntityFrameworkCore;
using Personal.Dara.Hub.Server.Models.Models;
using Personal.Dara.Hub.Server.Models.Models.Relationships;

namespace Personal.Dara.Hub.Server.Data.Context
{
    public class ServerDbContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<UserWorkspace> UserWorkspaces { get; set; }

        public ServerDbContext (DbContextOptions<ServerDbContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region UserWorkspace
            modelBuilder.Entity<UserWorkspace>()
                .HasKey(uw => new {uw.UserId, uw.WorkspaceId});

            modelBuilder.Entity<UserWorkspace>()
                .HasOne(uw => uw.User)
                .WithMany(uw => uw.Workspaces)
                .HasForeignKey(uw => uw.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserWorkspace>()
                .HasOne(uw => uw.Workspace)
                .WithMany(uw => uw.Users)
                .HasForeignKey(uw => uw.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserWorkspace>()
                .Property(uw => uw.UserRoleForWorkspace)
                .HasConversion<int>();
            #endregion

            #region User
            modelBuilder.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion<int>();

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();
            #endregion

            modelBuilder.Entity<Workspace>()
                .Property(w => w.Id)
                .ValueGeneratedOnAdd();
        }


    }
}
