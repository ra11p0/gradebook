using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Mailservice.MailTypesModels;

namespace Gradebook.Foundation.Mailservice.MailMessages;

public class RemindPasswordMailMessage : MailMessageBase<RemindPasswordMailTypeModel>
{
    public string AuthCode { get; set; }
    public RemindPasswordMailMessage(string targetGuid, string authCode) : base(targetGuid)
    {
        AuthCode = authCode;
    }
}
