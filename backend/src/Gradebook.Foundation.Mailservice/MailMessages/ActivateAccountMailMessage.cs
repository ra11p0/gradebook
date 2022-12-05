using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Mailservice.MailTypesModels;

namespace Gradebook.Foundation.Mailservice.MailMessages;

public class ActivateAccountMailMessage : MailMessageBase<ActivateAccountMailTypeModel>
{
    public string AuthCode { get; init; }
    public ActivateAccountMailMessage(string person, string authCode) : base(person)
    {
        AuthCode = authCode;
    }
}
