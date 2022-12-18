using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Hangfire;
using Gradebook.Foundation.Hangfire.Messages;

namespace Gradebook.Foundation.Mailservice;

public class MailClient : IMailClient
{
    private readonly string _sender;
    private readonly string? _senderName;
    private MailAddress Sender => new MailAddress(_sender, _senderName);
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<ISettingsQueries> _settingsQueries;
    private readonly IServiceProvider _serviceProvider;
    private readonly ServiceResolver<HangfireClient> _hangfireClient;
    public MailClient(IServiceProvider provider, string sender, string? senderName = null)
    {
        _identityLogic = provider.GetResolver<IIdentityLogic>();
        _settingsQueries = provider.GetResolver<ISettingsQueries>();
        _hangfireClient = provider.GetResolver<HangfireClient>();
        _sender = sender;
        _senderName = senderName;
        _serviceProvider = provider;
    }
    public async Task SendMail<Model>(MailMessageBase<Model> mailMessage)
    {
        var mailType = _serviceProvider.GetResolver<IMailType<Model>>().Service;
        var targetEmail = await GetTargetEmail(mailMessage.TargetGuid);
        var htmlString = await mailType.RenderBody(mailMessage, targetEmail);
        if (string.IsNullOrEmpty(mailMessage.TargetGuid)) throw new Exception("Target not specified");
        _hangfireClient.Service.SendMessage(new SendEmailWorkerMessage()
        {
            From = Sender.Address,
            To = targetEmail.Address,
            Subject = mailType.Subject,
            Message = htmlString
        });

    }
    private async Task<MailAddress> GetTargetEmail(string userGuid)
    {
        var userEmail = await _identityLogic.Service.GetEmailForUser(userGuid);
        if (userEmail is null) throw new Exception("User email should not be null");
        return new MailAddress(userEmail);
    }

}
