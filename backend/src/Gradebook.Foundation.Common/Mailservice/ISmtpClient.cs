using System.Net.Mail;

namespace Gradebook.Foundation.Common.Mailservice;

public interface ISmtpClient
{
    void Send(MailMessage message);
}
