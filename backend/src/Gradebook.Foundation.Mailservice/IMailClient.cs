namespace Gradebook.Foundation.Mailservice;

public interface IMailClient
{
    Task SendMail<M>(MailMessageBase<M> mailMessage);
}
