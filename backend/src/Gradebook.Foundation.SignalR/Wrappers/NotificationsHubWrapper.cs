using Gradebook.Foundation.Common.SignalR;
using Gradebook.Foundation.Common.SignalR.Notifications;
using Gradebook.Foundation.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Gradebook.Foundation.SignalR.Wrappers;

public class NotificationsHubWrapper : BaseHubWrapper<NotificationsHub, INotificationsHub>, INotificationsHubWrapper
{
    //  Here we implement outgoing signalr actions.
    public NotificationsHubWrapper(IHubContext<NotificationsHub, INotificationsHub> hub) : base(hub)
    {
    }

    public Task UserLoggedIn(string username, string userId)
    {
        return Hub.Clients.User(userId).UserLoggedIn(username);
    }
}
