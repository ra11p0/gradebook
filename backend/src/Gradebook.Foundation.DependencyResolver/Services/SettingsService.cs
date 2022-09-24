using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Settings.Database;
using Gradebook.Settings.Logic.Commands;
using Gradebook.Settings.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class SettingsService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<SettingsDatabaseContext>
            (options => options.UseMySql(configuration.GetConnectionString("DefaultAppDatabase"), new MySqlServerVersion(new Version(8, 30, 0))));

        services.AddScoped<ISettingsQueries, SettingsQueries>();
        services.AddScoped<ISettingsQueriesRepository, SettingsQueriesRepositoryCached>();
        services.AddScoped<SettingsQueriesRepository>();

        services.AddScoped<ISettingsCommands, SettingsCommands>();
        services.AddScoped<ISettingsCommandsRepository, SettingsCommandsRepositoryCached>();
        services.AddScoped<SettingsCommandsRepository>();
    }
}
