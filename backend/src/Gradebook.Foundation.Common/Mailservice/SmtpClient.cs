using LegacySmtpClient = System.Net.Mail.SmtpClient;

namespace Gradebook.Foundation.Common.Mailservice;

public class SmtpClient : LegacySmtpClient, ISmtpClient
{
    public SmtpClient(string? host, int port) : base(host, port)
    {
    }
}
