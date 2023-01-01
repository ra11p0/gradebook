using Gradebook.Foundation.Mailservice.MailMessages;
using Gradebook.Foundation.Mailservice.MailTypesModels;
using Microsoft.Extensions.Localization;

namespace Gradebook.Foundation.Mailservice.MailTypes;

public class RemindPasswordMailType : MailTypeBase<RemindPasswordMailMessage, RemindPasswordMailTypeModel>
{
    private readonly IStringLocalizer<RemindPasswordMailType> _localizer;
    public RemindPasswordMailType(IServiceProvider provider,
        IStringLocalizer<RemindPasswordMailType> localizer) : base(provider)
    {
        _localizer = localizer;
    }

    public override string Subject => _localizer["Subject"];

    protected override RemindPasswordMailTypeModel PrepareModel(RemindPasswordMailMessage message)
    {
        return new RemindPasswordMailTypeModel()
        {
            AuthCode = message.AuthCode,
            AccountGuid = message.TargetGuid
        };
    }
}
