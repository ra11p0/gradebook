using Gradebook.Foundation.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
namespace Gradebook.Foundation.SignalR;

public static class HubsMapper
{
    public static void MapHubs(this WebApplication? app)
    {
        if (app is null) throw new Exception("App is null!");
        app.MapHub<NotificationsHub>("api/signalr/notifications");
    }
}
