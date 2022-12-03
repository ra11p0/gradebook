using System.Globalization;
using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Common.Settings.Commands;
using Microsoft.Extensions.Configuration;
using Razor.Templating.Core;

namespace Gradebook.Foundation.Mailservice;

public abstract class MailTypeBase<Message, Model> : IMailType<Model> where Message : MailMessageBase<Model>
{

    private readonly ServiceResolver<ISettingsQueries> _settingsQueries;
    private readonly ServiceResolver<IRazorTemplateEngine> _razorTemplateEngine;
    private readonly ServiceResolver<IConfiguration> _config;
    public abstract string Subject { get; }

    public MailTypeBase(IServiceProvider provider)
    {
        _settingsQueries = provider.GetResolver<ISettingsQueries>();
        _razorTemplateEngine = provider.GetResolver<IRazorTemplateEngine>();
        _config = provider.GetResolver<IConfiguration>();
    }

    protected abstract Model PrepareModel(Message message);

    public async Task<string> RenderBody(MailMessageBase<Model> message, MailAddress targetEmail)
    {
        await SetUserLanguage(message.TargetGuid);
        Model model = PrepareModel((Message)message);
        var nameOfTemplate = this.GetType().Name;

        var viewbag = new Dictionary<string, object>();
        viewbag["baseUrl"] = _config.Service["Urls"];
        viewbag["targetEmail"] = targetEmail.Address;

        var html = await _razorTemplateEngine.Service.RenderAsync($"~/MailTypesTemplates/{nameOfTemplate}Template.cshtml", model, viewbag);

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
