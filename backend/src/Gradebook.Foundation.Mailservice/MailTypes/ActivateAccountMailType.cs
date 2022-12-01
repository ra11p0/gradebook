using Gradebook.Foundation.Mailservice.MailTypesModels;

namespace Gradebook.Foundation.Mailservice.MailTypes;

public class ActivateAccountMailType : MailBase<ActivateAccountMailTypeModel>
{
    public override string Subject => "super temat";
    public override string TargetUserGuid => "22b0e151-9558-4dad-87d1-948859c9ac62";
    public ActivateAccountMailType()
    {
        Model.Name = "hello!";
    }
}
