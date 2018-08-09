using Microsoft.EntityFrameworkCore;
using PhoneShopAPI.Models;

namespace PhoneShopAPI.DataLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Phone> PhoneItems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // build user mapping
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password).IsRequired();
            });

            // build role mapping
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(128).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // build user role mapping
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(k => k.RoleId);
                entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(k => k.UserId);
            });

            // build user token mapping
            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasOne(d => d.User)
                      .WithMany(p => p.UserTokens)
                      .HasForeignKey(k => k.UserId);
                entity.Property(e => e.AccessToken).HasMaxLength(450).IsRequired();
                entity.Property(e => e.RefreshToken).HasMaxLength(450).IsRequired();
            });
        }
    }
}