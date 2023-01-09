using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Hangfire;
using Gradebook.Foundation.Hangfire.Messages;
using Microsoft.AspNetCore.Diagnostics;

namespace Api;

public static class WebApplicationExtensions
{
    public static WebApplication UseEmailExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(context =>
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            var hangfireService = app.Services.GetResolver<IHangfireClient>();
            var config = app.Services.GetResolver<IConfiguration>();
            hangfireService.Service.SendMessage(new SendEmailWorkerMessage()
            {
                From = $"errors@gradebook-{config.Service["environment"]}.ovh",
                To = config.Service["adminEmail"],
                Subject = $"Gradebook exception on {config.Service["TargetUrl"]}{context.Request.Path}.",
                Message = @$"
                <small>Url</small>
                <a href='{config.Service["TargetUrl"]}{context.Request.Path}'>
                {config.Service["TargetUrl"]}{context.Request.Path}
                </a>
                <hr/>
                <small>Message</small>
                <div>
                    {exceptionHandlerPathFeature?.Error.Message}
                </div>
                <hr/>
                <small>Source</small>
                <div>
                    {exceptionHandlerPathFeature?.Error.Source}
                </div>
                <hr/>
                <small>StackTrace</small>
                <code>
                    {exceptionHandlerPathFeature?.Error.StackTrace}
                </code>
                <hr/>
                <small>InnerException</small>
                <code>
                    {exceptionHandlerPathFeature?.Error.InnerException}
                </code>
                <hr/>
                <small>TargetSite</small>
                <div>
                    {exceptionHandlerPathFeature?.Error.TargetSite}
                </div>
                "
            });
            return Task.CompletedTask;
        });
    });

        return app;
    }
}
