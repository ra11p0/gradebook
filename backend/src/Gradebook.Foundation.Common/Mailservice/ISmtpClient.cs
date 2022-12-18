using System.Net.Mail;

namespace Gradebook.Foundation.Common.Mailservice;

public interface ISmtpClient : IDisposable
{
    void Send(MailMessage message);
}
