using System.Net.Mail;

namespace Gradebook.Foundation.Common.Mailservice;

public interface IMailType<Model>
{
    public abstract string Subject { get; }
    Task<string> RenderBody(MailMessageBase<Model> message, MailAddress targetEmail);
}
