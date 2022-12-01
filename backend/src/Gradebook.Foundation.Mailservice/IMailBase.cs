using System.Net.Mail;

namespace Gradebook.Foundation.Mailservice;

public interface IMailBase
{
    public string TargetUserGuid { get; }
    public string Subject { get; }
    Task<string> RenderBody();
}
