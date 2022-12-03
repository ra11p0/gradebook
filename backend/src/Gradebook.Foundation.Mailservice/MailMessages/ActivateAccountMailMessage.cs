using Gradebook.Foundation.Mailservice.MailTypesModels;

namespace Gradebook.Foundation.Mailservice.MailMessages;

public class ActivateAccountMailMessage : MailMessageBase<ActivateAccountMailTypeModel>
{
    public ActivateAccountMailMessage(string person)
    {
        TargetGuid = person;
    }
}
