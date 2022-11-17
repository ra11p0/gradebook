using Microsoft.AspNetCore.SignalR;

namespace Gradebook.Foundation.Common.SignalR;

public abstract class BaseHubWrapper<T, I> where T : Hub<I> where I : class
{
    protected IHubContext<T, I> Hub { get; }
    public BaseHubWrapper(IHubContext<T, I> hub)
    {
        Hub = hub;
    }
}
