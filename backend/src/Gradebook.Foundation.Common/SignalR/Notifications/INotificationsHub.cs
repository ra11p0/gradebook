namespace Gradebook.Foundation.Common.SignalR.Notifications;

public interface INotificationsHub
{
    Task SendNotification(object notification);
    Task UserLoggedIn(string username);
}
