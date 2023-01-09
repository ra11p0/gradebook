using System.Net;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Mailservice;
using Gradebook.Foundation.Mailservice.MailTypes;
using Gradebook.Foundation.Mailservice.MailTypesModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class MailService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddScoped<ISmtpClient, SmtpClient>(o =>
        {
            var client = new SmtpClient(configuration["smtp:host"], int.Parse(configuration["smtp:port"]));
            var username = configuration["smtp:username"];
            var password = configuration["smtp:password"];
            if (username is not null && password is not null)
                client.Credentials = new NetworkCredential(username, password);
            return client;
        });

        services.AddScoped<IMailClient, MailClient>((e) =>
        {
            return new MailClient(e,
            configuration["smtp:defaultSender"],
            configuration["smtp:defaultSenderName"]
        );
        });

        //  services.AddRazorPages().AddViewLocalization();
        services.AddRazorTemplating();

        #region mail types

        services.AddScoped<IMailType<ActivateAccountMailTypeModel>, ActivateAccountMailType>();
        services.AddScoped<IMailType<RemindPasswordMailTypeModel>, RemindPasswordMailType>();

        #endregion
    }
}
