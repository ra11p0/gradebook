namespace Gradebook.Foundation.Common.Mailservice;

public abstract class MailMessageBase<Model>
{
    public string TargetGuid { get; init; }
    public MailMessageBase(string targetGuid)
    {
        TargetGuid = targetGuid;
    }
}
