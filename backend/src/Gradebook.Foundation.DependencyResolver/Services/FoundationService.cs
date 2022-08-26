using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class FoundationService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<FoundationDatabaseContext>
            (options => options.UseMySql(configuration.GetConnectionString("DefaultAppDatabase"), new MySqlServerVersion(new Version(8, 30, 0))));

        services.AddScoped<IFoundationQueries, FoundationQueries>();
        services.AddScoped<IFoundationQueriesRepository, FoundationQueriesRepositoryCached>();
        services.AddScoped<FoundationQueriesRepository>();

        services.AddScoped<IFoundationCommands, FoundationCommands>();
        services.AddScoped<IFoundationCommandsRepository, FoundationCommandsRepositoryCached>();
        services.AddScoped<FoundationCommandsRepository>();
    }
}
