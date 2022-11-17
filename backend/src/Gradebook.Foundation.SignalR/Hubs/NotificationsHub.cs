using Gradebook.Foundation.Common.SignalR.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Gradebook.Foundation.SignalR.Hubs;

[Authorize]
public class NotificationsHub : Hub<INotificationsHub>
{
    //  Here go incomming requests from clients. Check dep hub. We should handle them here.
}
