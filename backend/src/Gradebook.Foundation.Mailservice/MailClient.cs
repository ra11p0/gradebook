using System.Net;
using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Common.Settings.Commands;

namespace Gradebook.Foundation.Mailservice;

public class MailClient : IMailClient
{
    private readonly SmtpClient _client;
    private readonly string _sender;
    private readonly string? _senderName;
    private MailAddress Sender => new MailAddress(_sender, _senderName);
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<ISettingsQueries> _settingsQueries;
    private readonly IServiceProvider _serviceProvider;
    public MailClient(IServiceProvider provider, string host, int port, string sender, string? senderName, string? username, string? password)
    {
        _identityLogic = provider.GetResolver<IIdentityLogic>();
        _settingsQueries = provider.GetResolver<ISettingsQueries>();
        SmtpClient client = new SmtpClient(host, port);
        if (username is not null && password is not null)
            client.Credentials = new NetworkCredential(username, password);
        _client = client;
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
        var message = new MailMessage(
               Sender,
               targetEmail
               )
        {
            Body = htmlString,
            Subject = mailType.Subject,
            IsBodyHtml = true
        };
        _client.Send(message);
    }
    private async Task<MailAddress> GetTargetEmail(string userGuid)
    {
        var userEmail = await _identityLogic.Service.GetEmailForUser(userGuid);
        if (userEmail is null) throw new Exception("User email should not be null");
        return new MailAddress(userEmail);
    }

}
