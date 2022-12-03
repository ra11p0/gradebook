using System.Globalization;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Settings.Commands;
using Razor.Templating.Core;

namespace Gradebook.Foundation.Mailservice;

public abstract class MailTypeBase<Message, Model> : IMailType<Model> where Message : MailMessageBase<Model>
{
    public string Subject { get; set; }
    private readonly ServiceResolver<ISettingsQueries> _settingsQueries;
    private readonly ServiceResolver<IRazorTemplateEngine> _razorTemplateEngine;
    public MailTypeBase(IServiceProvider provider)
    {
        _settingsQueries = provider.GetResolver<ISettingsQueries>();
        _razorTemplateEngine = provider.GetResolver<IRazorTemplateEngine>();
        Subject = string.Empty;
    }

    protected abstract Task<Model> PrepareModel(Message message);

    public async Task<string> RenderBody(MailMessageBase<Model> message)
    {
        await SetUserLanguage(message.TargetGuid);
        Model model = await PrepareModel((Message)message);
        var nameOfTemplate = this.GetType().Name;

        var html = await _razorTemplateEngine.Service.RenderAsync($"~/MailTypesTemplates/{nameOfTemplate}Template.cshtml", model);

        return html;
    }
    private async Task SetUserLanguage(string userGuid)
    {
        if (string.IsNullOrEmpty(userGuid)) throw new Exception("Target not specified");
        var language = await _settingsQueries.Service.GetUserLanguage(userGuid);
        if (language is null) language = "en";
        var cultureInfo = new CultureInfo(language);
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}
