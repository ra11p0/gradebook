using Gradebook.Foundation.DependencyResolver.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver;
public static class DependencyInjector
{
    public static void Inject(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        services.AddHttpContextAccessor();
        AutoMapperService.Inject(services, configuration);
        IdentityService.Inject(services, configuration);
        FoundationService.Inject(services, configuration);
        PermissionsService.Inject(services, configuration);
        SettingsService.Inject(services, configuration);
        SwaggerService.Inject(services, configuration);
        SignalRService.Inject(services, configuration);
        HangfireService.Inject(services, configuration);
        MailService.Inject(services, configuration);
    }
}

