namespace Gradebook.Foundation.Mailservice;

public interface IMailType<Model>
{
    public string Subject { get; set; }
    Task<string> RenderBody(MailMessageBase<Model> message);
}
