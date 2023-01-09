using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Mailservice.MailTypes;
using Gradebook.Foundation.Mailservice.MailTypesModels;
using Microsoft.Extensions.DependencyInjection;
using FoundationIdentityLogic = Gradebook.Foundation.Identity.Logic.IdentityLogic;
using Moq;
using Gradebook.Foundation.Common;
using Microsoft.AspNetCore.Identity;
using Gradebook.Foundation.Identity.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Gradebook.Foundation.Mailservice;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using Gradebook.Foundation.Common.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Gradebook.Foundation.Hangfire;
using Gradebook.Foundation.Common.Hangfire;
using Gradebook.Foundation.Hangfire.Messages;
using Gradebook.Foundation.Hangfire.Workers;
using Gradebook.Foundation.Tests.Utils;
using Gradebook.Foundation.Database;

namespace Gradebook.Foundation.Tests.Identity;

[Category("Unit")]
public class IdentityLogic
{
    private ServiceCollection _serviceCollection
    {
        get
        {
            var services = new ServiceCollection();
            services.AddScoped<IConfiguration>(e => _configuration.Object);
            services.AddScoped<IMailClient, MailClient>(e => new MailClient(e, "fakeSender@email.com"));
            services.AddScoped<IMailType<ActivateAccountMailTypeModel>, ActivateAccountMailType>();
            services.AddScoped<ISettingsQueries>(e => _settingsQueries.Object);
            services.AddScoped<ISettingsCommands>(e => _settingsCommands.Object);
            services.AddScoped<ISmtpClient>(e => _smtpClient.Object);
            services.AddScoped<IIdentityLogic>(e => _identityLogicMocked.Object);
            services.AddScoped<IHangfireClient, FakeHangfireClient>();
            services.AddScoped<BaseHangfireWorker<SendEmailWorkerMessage>, SendEmailWorker>();
            services.AddScoped<Context>();
            services.AddDbContext<ApplicationIdentityDatabaseContext>(o =>
                {
                    o.UseInMemoryDatabase("fakeDb");
                    o.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            services.AddDbContext<FoundationDatabaseContext>(o =>
                {
                    o.UseInMemoryDatabase("fakeDb");
                    o.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDatabaseContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<Context>(e => _context);
            services.AddRazorTemplating();
            services.AddLogging();
            services.AddLocalization();
            return services;
        }
    }
    private IServiceProvider _serviceProvider => _serviceCollection.BuildServiceProvider();
    private Mock<ISmtpClient> _smtpClient { get; set; } = new();
    private Mock<ISettingsQueries> _settingsQueries { get; set; } = new();
    private Mock<IConfiguration> _configuration { get; set; } = new();
    private Mock<ISettingsCommands> _settingsCommands { get; set; } = new();
    private Mock<IIdentityLogic> _identityLogicMocked { get; set; } = new();
    private Context _context = new();
    [SetUp]
    public void SetUp()
    {
        var db = _serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        db.Database.EnsureDeleted();

        _smtpClient.Invocations.Clear();
    }
    [Test]
    public async Task ShouldSendActivationLinkOnRegister_ThenShouldResend()
    {
        var fakeEmail = "fake@email.com";
        var password = "P@55W0rD";
        _identityLogicMocked
            .Setup(e => e.GetEmailForUser(It.IsAny<string>()))
            .ReturnsAsync(fakeEmail);

        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var resp = await identityLogic.RegisterUser(fakeEmail, password, "en");
        Assert.That(resp.StatusCode, Is.EqualTo(200));

        _smtpClient.Verify(e =>
            e.Send(It.Is<MailMessage>(o => o.To.First().Address == fakeEmail)),
            Times.Exactly(1)
            );

        //  Then should send second email when user wants to log in to inactive account
        var resp2 = await identityLogic.LoginUser(fakeEmail, password);
        Assert.That(resp2.StatusCode, Is.EqualTo(302));
        _smtpClient.Verify(e =>
            e.Send(It.Is<MailMessage>(o => o.To.First().Address == fakeEmail)),
            Times.Exactly(2)
            );
    }
    [Test]
    public async Task ShouldActivateAccount()
    {
        _configuration.Setup(e => e["JWT:Secret"]).Returns("fakeSecretKey1234567890qwertyuiop");
        _configuration.Setup(e => e["JWT:TokenValidityInMinutes"]).Returns("1");

        var fakeEmail = "fake@email.com";
        var password = "P@55W0rD";
        var provider = _serviceProvider;
        var db = provider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        var identityLogic = new FoundationIdentityLogic(provider);
        _identityLogicMocked.Setup(e => e.GetEmailForUser(It.IsAny<string>())).ReturnsAsync(fakeEmail);

        var resp1 = await identityLogic.RegisterUser(fakeEmail, password, "pl");
        Assert.That(resp1.StatusCode, Is.EqualTo(200));

        var userId = db.Users!.First().Id;
        var code = db.AuthorizationCodes!.First();
        var resp2 = await identityLogic.VerifyUserEmail(userId, code.Code);
        Assert.That(resp2.StatusCode, Is.EqualTo(200));
        var user = db.Users.First();
        var authCode = db.AuthorizationCodes!.First();
        db.Entry(user).Reload();
        db.Entry(authCode).Reload();
        var resp3 = await identityLogic.LoginUser(fakeEmail, password);
        Assert.That(resp3.StatusCode, Is.EqualTo(200));

        Assert.That(user.EmailConfirmed);
        Assert.That(authCode.IsUsed);
    }
    [Test]
    public async Task ShouldCreateTokenAndAssignItToUser()
    {
        var provider = _serviceProvider;
        var fakeUser = new ApplicationUser();
        var db = provider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        db.Add(fakeUser);
        db.SaveChanges();
        var identityLogic = new FoundationIdentityLogic(provider);
        var resp = await identityLogic.CreateAuthCodeForUser(fakeUser.Id);

        Assert.That(resp.StatusCode, Is.EqualTo(200));
        Assert.That(db.Users!.Include(e => e.AuthorizationCodes).First(e => e.Id == fakeUser.Id).AuthorizationCodes!.Any(e => e.Code == resp.Response));
    }
    [Test]
    [Obsolete]
    public async Task ShouldReturnAuthCodeInvalid()
    {
        var provider = _serviceProvider;
        var fakeUser = new ApplicationUser();
        var db = provider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        db.Add(fakeUser);
        db.SaveChanges();
        var identityLogic = new FoundationIdentityLogic(provider);

        var resp1 = await identityLogic.CreateAuthCodeForUser(fakeUser.Id);
        Assert.That(resp1.StatusCode, Is.EqualTo(200));

        var resp2 = await identityLogic.IsAuthCodeValid(fakeUser.Id, resp1.Response!);
        Assert.That(resp2.StatusCode, Is.EqualTo(200));

        Time.SetFakeUtcNow(Time.UtcNow.AddDays(1));

        var resp3 = await identityLogic.IsAuthCodeValid(fakeUser.Id, resp1.Response!);
        Assert.That(resp3.StatusCode, Is.EqualTo(404));
    }
    [Test]
    public async Task ShouldMakeCodeInvalidWhenUsed()
    {
        var provider = _serviceProvider;
        var fakeUser = new ApplicationUser();
        var db = provider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        db.Add(fakeUser);
        db.SaveChanges();
        var identityLogic = new FoundationIdentityLogic(provider);

        var resp1 = await identityLogic.CreateAuthCodeForUser(fakeUser.Id);
        Assert.That(resp1.StatusCode, Is.EqualTo(200));

        var resp2 = await identityLogic.IsAuthCodeValid(fakeUser.Id, resp1.Response!);
        Assert.That(resp2.StatusCode, Is.EqualTo(200));

        var resp3 = await identityLogic.UseAuthCode(fakeUser.Id, resp1.Response!);
        Assert.That(resp3.StatusCode, Is.EqualTo(200));

        var resp4 = await identityLogic.IsAuthCodeValid(fakeUser.Id, resp1.Response!);
        Assert.That(resp4.StatusCode, Is.EqualTo(404));
    }
}
