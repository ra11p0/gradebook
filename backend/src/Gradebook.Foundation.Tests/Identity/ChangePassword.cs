using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Hangfire;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Hangfire;
using Gradebook.Foundation.Hangfire.Messages;
using Gradebook.Foundation.Hangfire.Workers;
using Gradebook.Foundation.Identity.Models;
using Gradebook.Foundation.Mailservice;
using Gradebook.Foundation.Mailservice.MailMessages;
using Gradebook.Foundation.Mailservice.MailTypes;
using Gradebook.Foundation.Mailservice.MailTypesModels;
using Gradebook.Foundation.Tests.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using FoundationIdentityLogic = Gradebook.Foundation.Identity.Logic.IdentityLogic;

namespace Gradebook.Foundation.Tests.Identity;

public class ChangePassword
{
    private ServiceCollection _serviceCollection
    {
        get
        {
            var services = new ServiceCollection();
            services.AddScoped<IConfiguration>(e => _configuration.Object);
            services.AddScoped<IMailClient, MailClient>(e => new MailClient(e, "fakeSender@email.com"));
            services.AddScoped<ISmtpClient>(e => _smtpClient.Object);
            services.AddScoped<IIdentityLogic>(e => _identityLogicMocked.Object);
            services.AddScoped<IHangfireClient, FakeHangfireClient>();
            services.AddScoped<BaseHangfireWorker<SendEmailWorkerMessage>, SendEmailWorker>();
            services.AddScoped<Context>();
            services.AddScoped<ISettingsQueries>(e => _settingsQueries.Object);
            services.AddScoped<ISettingsCommands>(e => _settingsCommands.Object);
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
            services.AddScoped<IMailType<RemindPasswordMailTypeModel>, RemindPasswordMailType>();
            services.AddScoped<IMailType<ActivateAccountMailTypeModel>, ActivateAccountMailType>();
            services.AddScoped<IHttpContextAccessor>(e => _httpContextAccessor.Object);
            return services;
        }
    }
    private IServiceProvider _serviceProvider => _serviceCollection.BuildServiceProvider();
    private Mock<ISmtpClient> _smtpClient { get; set; } = new();
    private Mock<IConfiguration> _configuration { get; set; } = new();
    private Mock<IIdentityLogic> _identityLogicMocked { get; set; } = new();
    private Mock<ISettingsQueries> _settingsQueries { get; set; } = new();
    private Mock<ISettingsCommands> _settingsCommands { get; set; } = new();
    private Mock<IHttpContextAccessor> _httpContextAccessor { get; set; } = new();
    private Context _context = new();
    [SetUp]
    public void SetUp()
    {
        var db = _serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        db.Database.EnsureDeleted();
        var foundationDb = _serviceProvider.GetResolver<FoundationDatabaseContext>().Service;
        foundationDb.Database.EnsureDeleted();

        _smtpClient.Invocations.Clear();
    }
    [Test]
    public async Task ShouldSendRemindPasswordLink()
    {
        var fakeEmail = "fake@email.com";
        _identityLogicMocked
            .Setup(e => e.GetEmailForUser(It.IsAny<string>()))
            .ReturnsAsync(fakeEmail);

        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var registerResp = await identityLogic.RegisterUser(fakeEmail, "!QAZ2wsx", "en");
        Assert.That(registerResp.Status, "Could not register account");
        _smtpClient.Invocations.Clear();
        var resp = await identityLogic.RemindPassword(fakeEmail);

        Assert.That(resp.StatusCode, Is.EqualTo(200));
        _smtpClient.Verify(e =>
            e.Send(
                It.Is<MailMessage>(o =>
                o.To.First().Address == fakeEmail)),
            Times.Exactly(1)
            , "Only one email should be sent at the moment");
    }
    [Test]
    public async Task ShouldChangePassword()
    {
        var foundationDb = _serviceProvider.GetResolver<FoundationDatabaseContext>().Service;
        var identityDb = _serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        var fakeEmail = "fake@email.com";
        _identityLogicMocked
            .Setup(e => e.GetEmailForUser(It.IsAny<string>()))
            .ReturnsAsync(fakeEmail);

        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var registerResp = await identityLogic.RegisterUser(fakeEmail, "!QAZ2wsx", "en");
        var timeStampBeforeSendRemindPassword = Time.UtcNow;
        var firstPassword = identityDb.Users.First().PasswordHash;
        Assert.That(registerResp.Status, "Could not register account");
        _smtpClient.Invocations.Clear();

        var resp = await identityLogic.RemindPassword(fakeEmail);

        Assert.That(resp.StatusCode, Is.EqualTo(200), "Could not request remind password email");
        _smtpClient.Verify(e =>
            e.Send(
                It.Is<MailMessage>(o =>
                o.To.First().Address == fakeEmail)),
            Times.Exactly(1)
            );
        var mailHistoryLog = foundationDb.MailHistory!.First(e =>
            e.SendDateTime >= timeStampBeforeSendRemindPassword &&
            e.To == fakeEmail &&
            e.MessageType == "RemindPasswordMailMessage");

        var messagePayload = JsonConvert.DeserializeObject<RemindPasswordMailMessage>(mailHistoryLog!.PayloadJson);

        var remindPasswordAuthCode = messagePayload.AuthCode;
        var remindPasswordUserId = messagePayload.TargetGuid;

        var respSetPassword = await identityLogic.SetNewPassword(remindPasswordUserId, remindPasswordAuthCode, "!QAZ2wsxNEW", "!QAZ2wsxNEW");
        Assert.That(respSetPassword.StatusCode, Is.EqualTo(200), "Failed setting new password.");
        identityDb.Entry(identityDb.Users.First()).Reload();
        Assert.That(identityDb.Users.First().PasswordHash != firstPassword, "Password has not been changed.");
    }
    [Test]
    public async Task ShouldNotChangePassword_WrongAuth()
    {
        var foundationDb = _serviceProvider.GetResolver<FoundationDatabaseContext>().Service;
        var identityDb = _serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        var fakeEmail = "fake@email.com";
        _identityLogicMocked
            .Setup(e => e.GetEmailForUser(It.IsAny<string>()))
            .ReturnsAsync(fakeEmail);

        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var registerResp = await identityLogic.RegisterUser(fakeEmail, "!QAZ2wsx", "en");
        var firstPassword = identityDb.Users.First().PasswordHash;
        Assert.That(registerResp.Status, "Could not register account");
        _smtpClient.Invocations.Clear();
        var timeStampBeforeSendRemindPassword = Time.UtcNow;
        var resp = await identityLogic.RemindPassword(fakeEmail);

        Assert.That(resp.StatusCode, Is.EqualTo(200), "Could not request remind password email");
        _smtpClient.Verify(e =>
            e.Send(
                It.Is<MailMessage>(o =>
                o.To.First().Address == fakeEmail)),
            Times.Exactly(1)
            );
        var mailHistoryLog = foundationDb.MailHistory!.First(e =>
            e.SendDateTime >= timeStampBeforeSendRemindPassword &&
            e.To == fakeEmail &&
            e.MessageType == "RemindPasswordMailMessage");


        var messagePayload = JsonConvert.DeserializeObject<RemindPasswordMailMessage>(mailHistoryLog!.PayloadJson);
        var remindPasswordUserId = messagePayload.TargetGuid;

        var respSetPassword = await identityLogic.SetNewPassword(remindPasswordUserId, "fakeAuth", "!QAZ2wsxNEW", "!QAZ2wsxNEW");
        Assert.That(respSetPassword.StatusCode, Is.EqualTo(404), "Failed setting new password.");
        identityDb.Entry(identityDb.Users.First()).Reload();
        Assert.That(identityDb.Users.First().PasswordHash == firstPassword, "Password has been changed.");
    }
    [Test]
    public async Task ShouldNotChangePassword_PasswordConfirmWrong()
    {
        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var respSetPassword = await identityLogic.SetNewPassword("fakeUid", "fakeauth", "!QAZ2wsxNEW", "!QAZ2wsxNE");
        Assert.That(respSetPassword.StatusCode, Is.EqualTo(400), "Failed setting new password.");
    }
    [Test]
    public async Task ShouldNotChangePasswordAuthorized_PasswordConfirmWrong()
    {
        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var respSetPassword = await identityLogic.SetNewPasswordAuthorized("QAZ2wsxNEW", "!QAZ2wsxNEW", "!QAZ2wsxNE");
        Assert.That(respSetPassword.StatusCode, Is.EqualTo(400), "Failed setting new password.");
    }
    [Test]
    public async Task ShouldSetNewPasswordAuthorizedUser()
    {
        var foundationDb = _serviceProvider.GetResolver<FoundationDatabaseContext>().Service;
        var identityDb = _serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        var fakeEmail = "fake@email.com";
        _identityLogicMocked
            .Setup(e => e.GetEmailForUser(It.IsAny<string>()))
            .ReturnsAsync(fakeEmail);

        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var registerResp = await identityLogic.RegisterUser(fakeEmail, "!QAZ2wsx", "en");
        var account = identityDb.Users.First();
        var oldPassword = account.PasswordHash;
        _context.UserId = account.Id;
        Assert.That(registerResp.Status, "Could not register account");
        _smtpClient.Invocations.Clear();
        var timeStampBeforeSendRemindPassword = Time.UtcNow;
        var resp = await identityLogic.SetNewPasswordAuthorized("!QAZ2wsxNEW", "!QAZ2wsxNEW", "!QAZ2wsx");
        Assert.That(resp.StatusCode, Is.EqualTo(200), "Could not set new password");
        identityDb.Entry(identityDb.Users.First()).Reload();
        Assert.That(identityDb.Users.First().PasswordHash != oldPassword, "Password has not been changed.");
    }
    [Test]
    public async Task ShouldNotSetNewPasswordAuthorizedUser_WrongOldPassword()
    {
        var foundationDb = _serviceProvider.GetResolver<FoundationDatabaseContext>().Service;
        var identityDb = _serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>().Service;
        var fakeEmail = "fake@email.com";
        _identityLogicMocked
            .Setup(e => e.GetEmailForUser(It.IsAny<string>()))
            .ReturnsAsync(fakeEmail);

        var identityLogic = new FoundationIdentityLogic(_serviceProvider);
        var registerResp = await identityLogic.RegisterUser(fakeEmail, "!QAZ2wsx", "en");
        var account = identityDb.Users.First();
        var oldPassword = account.PasswordHash;
        _context.UserId = account.Id;
        Assert.That(registerResp.Status, "Could not register account");
        _smtpClient.Invocations.Clear();
        var timeStampBeforeSendRemindPassword = Time.UtcNow;
        var resp = await identityLogic.SetNewPasswordAuthorized("!QAZ2wsxNEW", "!QAZ2wsxNEW", "!QAZ2sswsx");
        Assert.That(resp.StatusCode, Is.EqualTo(403));
        identityDb.Entry(identityDb.Users.First()).Reload();
        Assert.That(identityDb.Users.First().PasswordHash == oldPassword, "Password has been changed.");
    }
}
