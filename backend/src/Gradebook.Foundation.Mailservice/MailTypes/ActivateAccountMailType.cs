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

    protected async override Task<ActivateAccountMailTypeModel> PrepareModel(ActivateAccountMailMessage message)
    {
        var str = _localizer["hello"];
        return new ActivateAccountMailTypeModel() { Name = "hejka!" };
    }
}
