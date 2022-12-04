using System.Security.Claims;
using Gradebook.Foundation.Common.Hangfire;
using Microsoft.AspNetCore.Http;

namespace Gradebook.Foundation.Common;

public class Context
{
    public string? UserId { get; set; }
    public Context()
    { }
    public Context(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor is not null && httpContextAccessor.HttpContext is not null)
        {
            UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return;
        }
        if (WorkerContext.Context is not null)
        {
            var ctx = WorkerContext.Context;
            UserId = ctx.UserId;
            return;
        }
    }
}
