using Gradebook.Foundation.Common.SignalR.Notifications;
using Gradebook.Foundation.SignalR.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class SignalRService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSignalR(opt =>
        {
            //opt.KeepAliveInterval = TimeSpan.FromSeconds(15);
            opt.EnableDetailedErrors = true;
        });
        services.AddScoped<INotificationsHubWrapper, NotificationsHubWrapper>();
    }
}
