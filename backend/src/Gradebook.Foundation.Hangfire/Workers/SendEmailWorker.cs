using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Hangfire;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Domain.Models.System;
using Gradebook.Foundation.Hangfire.Messages;

namespace Gradebook.Foundation.Hangfire.Workers;

public class SendEmailWorker : BaseHangfireWorker<SendEmailWorkerMessage>
{
    private readonly ServiceResolver<ISmtpClient> _client;
    private readonly ServiceResolver<FoundationDatabaseContext> _foundationDbContext;

    public SendEmailWorker(IServiceProvider provider)
    {

        _client = provider.GetResolver<ISmtpClient>();
        _foundationDbContext = provider.GetResolver<FoundationDatabaseContext>();
    }
    public async override Task DoJob(SendEmailWorkerMessage message)
    {
        var mailMessage = new MailMessage(
       new MailAddress(message.From!),
       new MailAddress(message.To!)
       )
        {
            Body = message.Message,
            Subject = message.Subject,
            IsBodyHtml = true
        };
        using var client = _client.Service;
        client.Send(mailMessage);
        await _foundationDbContext.Service.MailHistory!.AddAsync(new Email()
        {
            From = message.From!,
            To = message.To!,
            Subject = message.Subject!,
            Message = message.Message!
        });
        await _foundationDbContext.Service.SaveChangesAsync();
    }
}
