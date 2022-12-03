namespace Gradebook.Foundation.Mailservice;

public abstract class MailMessageBase<Model>
{
    public string TargetGuid { get; set; } = string.Empty;
}
