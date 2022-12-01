namespace Gradebook.Foundation.Mailservice;

public interface IMailClient
{
    Task SendMail<T>(T mail) where T : IMailBase;
}
