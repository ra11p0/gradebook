using Gradebook.Foundation.Common.Hangfire;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Hangfire.Messages;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.SignalR.Notifications;

namespace Gradebook.Foundation.Hangfire.Workers;

public class NotificationsWorker : BaseHangfireWorker<NotificationsWorkerMessage>
{
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<INotificationsHubWrapper> _notificationsHubWrapper;

    public NotificationsWorker(Context context, IServiceProvider provider)
    {
        _identityLogic = provider.GetResolver<IIdentityLogic>();
        _notificationsHubWrapper = provider.GetResolver<INotificationsHubWrapper>();
    }

    public override async Task DoJob(NotificationsWorkerMessage message)
    {
        var uid = message.Context?.UserId;
        var uid2 = await _identityLogic.Service.CurrentUserId();
        await _notificationsHubWrapper.Service.UserLoggedIn("job!", uid2.Response!);
    }
}
