using Gradebook.Foundation.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
namespace Gradebook.Foundation.SignalR;

public class HubsMapper
{
    public static void MapHubs(WebApplication? app)
    {
        if (app is null) throw new Exception("App is null!");
        app.MapHub<NotificationsHub>("api/signalr/notifications");
    }
}
