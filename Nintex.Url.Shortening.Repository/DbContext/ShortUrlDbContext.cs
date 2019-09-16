using Microsoft.EntityFrameworkCore;
using Nintex.Url.Shortening.Core.DbModels;

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
            modelBuilder.Entity<ShortUrlModel>()
                .HasIndex(su => su.Key)
                .IsUnique();
        }
    }
}
