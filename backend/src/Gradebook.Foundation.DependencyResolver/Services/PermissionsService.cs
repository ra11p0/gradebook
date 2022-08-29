using Gradebook.Foundation.Common.Permissions.Commands;
using Gradebook.Foundation.Common.Permissions.Queries;
using Gradebook.Permissions.Database;
using Gradebook.Permissions.Logic.Commands;
using Gradebook.Permissions.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class PermissionsService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<PermissionsDatabaseContext>
            (options => options.UseMySql(configuration.GetConnectionString("DefaultAppDatabase"), new MySqlServerVersion(new Version(8, 30, 0))));

        services.AddScoped<IPermissionsQueries, PermissionsQueries>();
        services.AddScoped<IPermissionsQueriesRepository, PermissionsQueriesRepositoryCached>();
        services.AddScoped<PermissionsQueriesRepository>();

        services.AddScoped<IPermissionsCommands, PermissionsCommands>();
        services.AddScoped<IPermissionsCommandsRepository, PermissionsCommandsRepositoryCached>();
        services.AddScoped<PermissionsCommandsRepository>();
    }
}
