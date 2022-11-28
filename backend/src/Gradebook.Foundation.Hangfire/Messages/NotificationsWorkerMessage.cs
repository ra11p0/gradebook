using Gradebook.Foundation.Common.Hangfire;

namespace Gradebook.Foundation.Hangfire.Messages;

public class NotificationsWorkerMessage : BaseHangfireWorkerMessage
{
    public string SomeProperty { get; set; }
    public NotificationsWorkerMessage(string someProperty)
    {
        SomeProperty = someProperty;
    }
}
