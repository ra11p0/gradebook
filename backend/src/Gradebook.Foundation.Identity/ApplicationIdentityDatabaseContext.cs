using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gradebook.Foundation.Identity.Models;

public class ApplicationIdentityDatabaseContext: IdentityDbContext<ApplicationUser>
{
    public ApplicationIdentityDatabaseContext()
    {}
    public ApplicationIdentityDatabaseContext(
        DbContextOptions<ApplicationIdentityDatabaseContext> options) 
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var cfg = builder.Build();
            var cn = cfg.GetConnectionString("DefaultAppDatabase");
            optionsBuilder.UseMySql(cn, new MySqlServerVersion(new Version(8, 0, 0)));
        }
    }
}
