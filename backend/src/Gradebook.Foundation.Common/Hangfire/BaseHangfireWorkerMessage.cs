namespace Gradebook.Foundation.Common.Hangfire;

public abstract class BaseHangfireWorkerMessage
{
    public Context? Context { get; set; }
}
