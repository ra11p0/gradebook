namespace Gradebook.Foundation.Common.Hangfire;

public abstract class BaseHangfireWorker<T> where T : BaseHangfireWorkerMessage
{
    public async Task DoJobWithContext(T message)
    {
        WorkerContext.Context = message.Context;
        await DoJob(message);
        WorkerContext.Context = null;
    }
    public abstract Task DoJob(T message);
}
