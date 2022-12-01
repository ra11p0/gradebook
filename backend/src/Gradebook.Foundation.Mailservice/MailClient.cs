using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;

namespace Gradebook.Foundation.Mailservice;

public class MailClient : IMailClient
{
    private readonly SmtpClient _client;
    private readonly string _sender;
    private readonly string? _senderName;
    private MailAddress Sender => new MailAddress(_sender, _senderName);
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    public MailClient(IServiceProvider provider, string host, int port, string sender, string? senderName, string? username, string? password)
    {
        _identityLogic = provider.GetResolver<IIdentityLogic>();
        SmtpClient client = new SmtpClient(host, port);
        if (username is not null && password is not null)
            client.Credentials = new NetworkCredential(username, password);
        _client = client;
        _sender = sender;
        _senderName = senderName;
    }
    public async Task SendMail<T>(T mail) where T : IMailBase
    {
        var htmlString = await mail.RenderBody();
        var targetEmail = await GetTargetEmail(mail.TargetUserGuid);
        var message = new MailMessage(
               Sender,
               targetEmail
               )
        {
            Body = htmlString,
            Subject = mail.Subject,
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
    private void SetUserLanguage(string userGuid)
    {

    }

}
