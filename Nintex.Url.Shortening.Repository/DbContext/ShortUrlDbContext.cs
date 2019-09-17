using System;
using Microsoft.EntityFrameworkCore;
using Nintex.Url.Shortening.Core.DbModels;
using Nintex.Url.Shortening.Core.Utility;

namespace Nintex.Url.Shortening.Repository.DbContext
{
    public class ShortUrlDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options) : base(options)
        {

        }

        public DbSet<AccountModel> Account { get; set; }
        public DbSet<ShortUrlModel> ShortUrl { get; set; }
        public DbSet<ShortUrlLogEntryModel> ShortUrlVisitLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().HasKey(x => x.Id);
            modelBuilder.Entity<ShortUrlModel>().HasKey(x => x.Id);
            modelBuilder.Entity<ShortUrlLogEntryModel>().HasKey(x => x.Id);
            
            modelBuilder.Entity<AccountModel>().HasData(GetAdminAccount());

            modelBuilder.Entity<ShortUrlModel>()
                .HasIndex(su => su.Key)
                .IsUnique();
        }

        private AccountModel GetAdminAccount()
        {
            PasswordHasher.CreatePasswordHash("admin", out var passwordHash, out var passwordSalt);
            return new AccountModel
            {
                Id = 1,
                Username = "admin",
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                UpdateDate = DateTime.UtcNow
            };
        }
    }
}
