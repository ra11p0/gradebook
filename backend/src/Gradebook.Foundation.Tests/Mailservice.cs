using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Hangfire;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Hangfire;
using Gradebook.Foundation.Hangfire.Messages;
using Gradebook.Foundation.Hangfire.Workers;
using Gradebook.Foundation.Mailservice;
using Gradebook.Foundation.Mailservice.MailMessages;
using Gradebook.Foundation.Mailservice.MailTypes;
using Gradebook.Foundation.Mailservice.MailTypesModels;
using Gradebook.Foundation.Tests.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests;

[Category("Unit")]
public class Mailservice
{
    #region const
    private const string HOST = "fakeHost";
    private const int PORT = 25;
    private const string SENDER = "fakeSender@sender.com";
    #endregion
    private ServiceCollection _serviceCollection
    {
        get
        {
            var services = new ServiceCollection();
            services.AddScoped<ISmtpClient>(o => _smtpClient.Object);
            services.AddScoped<IIdentityLogic>(o => _identityLogic.Object);
            services.AddScoped<IMailType<ActivateAccountMailTypeModel>, ActivateAccountMailType>();
            services.AddScoped<ISettingsQueries>(e => _settingsQueries.Object);
            services.AddScoped<IConfiguration>(e => _configuration.Object);
            services.AddRazorTemplating();
            services.AddLogging();
            services.AddLocalization();
            services.AddScoped<IHangfireClient, FakeHangfireClient>();
            services.AddScoped<BaseHangfireWorker<SendEmailWorkerMessage>, SendEmailWorker>();
            services.AddScoped<Context>();
            services.AddDbContext<FoundationDatabaseContext>(o =>
                {
                    o.UseInMemoryDatabase("fakeDb");
                    o.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            return services;
        }
    }
    private IServiceProvider _serviceProvider => _serviceCollection.BuildServiceProvider();
    private Mock<ISmtpClient> _smtpClient { get; set; } = new();
    private Mock<IIdentityLogic> _identityLogic { get; set; } = new();
    private Mock<ISettingsQueries> _settingsQueries { get; set; } = new();
    private Mock<IConfiguration> _configuration { get; set; } = new();
    private Mock<IHangfireClient> _hangfireClient { get; set; } = new();
    [Test]
    public async Task ShouldSendMail()
    {
        var fakeTargetEmail = "fake@target.email";
        var fakeUserId = "fakeUid";
        _configuration.Setup(e => e["smtp:host"]).Returns(HOST);
        _smtpClient.Setup(e => e.Send(It.IsAny<MailMessage>()));
        _identityLogic.Setup(e => e.GetEmailForUser(fakeUserId)).ReturnsAsync(fakeTargetEmail);

        var mailClient = new MailClient(_serviceProvider, SENDER, null);
        await mailClient.SendMail(new ActivateAccountMailMessage(fakeUserId, "fakeAuthCode"));

        _smtpClient.Verify(e =>
            e.Send(It.Is<MailMessage>(o => o.From!.Address == SENDER && o.To.First().Address == fakeTargetEmail)),
            Times.Exactly(1)
            );
    }
}
