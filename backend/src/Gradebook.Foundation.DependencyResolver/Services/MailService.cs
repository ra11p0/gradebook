using Gradebook.Foundation.Mailservice;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class MailService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddScoped<IMailClient, MailClient>((e) =>
        {
            return new MailClient(e,
            configuration["smtp:host"],
            int.Parse(configuration["smtp:port"]),
            configuration["smtp:defaultSender"],
            configuration["smtp:defaultSenderName"],
            configuration["smtp:username"],
            configuration["smtp:password"]
        );
        });
    }
}
