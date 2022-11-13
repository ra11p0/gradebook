using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class HangfireService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddHangfire(cfg => cfg
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseStorage(new MySqlStorage(
             configuration.GetConnectionString("DefaultAppDatabase"),
             new MySqlStorageOptions
             {
                 QueuePollInterval = TimeSpan.FromSeconds(15),
                 JobExpirationCheckInterval = TimeSpan.FromHours(1),
                 CountersAggregateInterval = TimeSpan.FromMinutes(5),
                 PrepareSchemaIfNecessary = true,
                 DashboardJobListLimit = 50000,
                 TransactionTimeout = TimeSpan.FromMinutes(1),
                 TablesPrefix = "Hangfire"
             })));
        services.AddHangfireServer();
    }
    public static void MapHangfireEndpoint(WebApplication app)
    {
        app.MapHangfireDashboard();
    }
}
