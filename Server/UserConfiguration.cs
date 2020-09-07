using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server
{
    public class UserConfigurationDbContext : DbContext
    {
        public UserConfigurationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(t => new { t.RoleName, t.UserAccount });

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserAccount);

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.RoleName);

            modelBuilder.Entity<UserClaim>()
                .HasOne(x => x.UserEntity)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.UserId);
        }
    }
}