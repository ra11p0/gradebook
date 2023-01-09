using Gradebook.Foundation.Domain;
using Gradebook.Settings.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gradebook.Settings.Database;

public class SettingsDatabaseContext : DbContext
{
    public DbSet<Setting>? Settings { get; set; }
    public DbSet<AccountSetting>? AccountSettings { get; set; }
    public SettingsDatabaseContext()
    {

    }
    public SettingsDatabaseContext(DbContextOptions<SettingsDatabaseContext> options)
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
                e => e.MigrationsHistoryTable("__SettingsMigrationsHistory")
            );
        }
    }

    public async Task Migrate()
    {
        await this.Database.MigrateAsync();
    }
}
