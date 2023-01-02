using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Commands.Repositories;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Foundation.Logic.Queries.Repositories;
using Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;
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

        //  Theres no cache implemented yet, but IBaseRepositoryCached exists, and is implemented. There is type 'object' used as a placeholder of 
        //  cache machine class, so there is need to add type 'object' to dependency injection
        //  TODO: Remove when cache implemented
        services.AddScoped<object>();
        services.AddScoped<IFoundationQueries, FoundationQueries>();
        services.AddScoped<IFoundationQueriesRepository, FoundationQueriesRepositoryCached>();
        services.AddScoped<FoundationQueriesRepository>();

        services.AddScoped<IFoundationCommands, FoundationCommands>();
        services.AddScoped<IFoundationCommandsRepository, FoundationCommandsRepositoryCached>();
        services.AddScoped<FoundationCommandsRepository>();

        services.AddScoped<IFoundationPermissionsLogic, FoundationPermissionsLogic>();

        services.AddScoped<Context>();

        services.AddLocalization(options => options.ResourcesPath = "Resources");
    }
}
