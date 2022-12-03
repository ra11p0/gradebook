namespace Gradebook.Foundation.Common.Mailservice;

public interface IMailClient
{
    Task SendMail<M>(MailMessageBase<M> mailMessage);
}
