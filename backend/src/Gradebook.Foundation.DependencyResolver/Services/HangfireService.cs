using Gradebook.Foundation.Common.Hangfire;
using Gradebook.Foundation.Hangfire;
using Gradebook.Foundation.Hangfire.Messages;
using Gradebook.Foundation.Hangfire.Workers;
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

        services.AddScoped<HangfireClient>();

        #region workers 

        services.AddScoped<BaseHangfireWorker<NotificationsWorkerMessage>, NotificationsWorker>();
        services.AddScoped<BaseHangfireWorker<SendEmailWorkerMessage>, SendEmailWorker>();

        #endregion
    }
    public static void MapHangfireEndpoint(WebApplication app)
    {
        app.MapHangfireDashboard();
    }
}
