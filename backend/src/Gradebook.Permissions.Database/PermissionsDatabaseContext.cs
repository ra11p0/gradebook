using Gradebook.Foundation.Domain;
using Gradebook.Permissions.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gradebook.Permissions.Database;

public class PermissionsDatabaseContext : DbContext
{
    public DbSet<Permission>? Permissions { get; set; }
    public PermissionsDatabaseContext()
    {

    }
    public PermissionsDatabaseContext(DbContextOptions<PermissionsDatabaseContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        FoundationModelsMappings.ConfigureModels(builder);
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
            optionsBuilder.UseMySql(
                cn,
                new MySqlServerVersion(new Version(8, 30, 0)),
                e => e.MigrationsHistoryTable("__PermissionsMigrationsHistory")
            );
        }
    }

    public async Task Migrate()
    {
        await this.Database.MigrateAsync();
    }
}
