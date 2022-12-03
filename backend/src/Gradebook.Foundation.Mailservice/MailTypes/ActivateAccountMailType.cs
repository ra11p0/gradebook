using Gradebook.Foundation.Mailservice.MailMessages;
using Gradebook.Foundation.Mailservice.MailTypesModels;
using Microsoft.Extensions.Localization;

namespace Gradebook.Foundation.Mailservice.MailTypes;

public class ActivateAccountMailType : MailTypeBase<ActivateAccountMailMessage, ActivateAccountMailTypeModel>
{
    private readonly IStringLocalizer<ActivateAccountMailType> _localizer;
    public ActivateAccountMailType(IServiceProvider provider, IStringLocalizer<ActivateAccountMailType> localizer) : base(provider)
    {
        _localizer = localizer;
    }

    public override string Subject { get => _localizer["Subject"]; }

    protected override ActivateAccountMailTypeModel PrepareModel(ActivateAccountMailMessage message)
    {
        return new ActivateAccountMailTypeModel()
        {
            AuthCode = message.AuthCode,
            AccountGuid = message.TargetGuid
        };
    }
}
