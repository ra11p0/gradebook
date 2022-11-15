namespace Gradebook.Foundation.Common.SignalR.Notifications;

public interface INotificationsHubWrapper
{
    Task UserLoggedIn(string username, string userId);
}
