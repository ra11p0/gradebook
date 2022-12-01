using System.Net.Mail;
using Razor.Templating.Core;

namespace Gradebook.Foundation.Mailservice;

public abstract class MailBase<T> : IMailBase where T : new()
{
    public T Model { get; set; } = new();
    public abstract string Subject { get; }
    public abstract string TargetUserGuid { get; }

    public async Task<string> RenderBody()
    {
        var nameOfTemplate = this.GetType().Name;
        var html = await RazorTemplateEngine.RenderAsync($"~/MailTypesTemplates/{nameOfTemplate}Template.cshtml", Model);
        return html;
    }
}
