using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Hangfire;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.Hangfire;

public class HangfireClient
{
    private readonly Context _context;
    private readonly IServiceProvider _serviceProvider;
    public HangfireClient(Context context, IServiceProvider provider)
    {
        _context = context;
        _serviceProvider = provider;
    }
    public void SendMessage<I>(I message) where I : BaseHangfireWorkerMessage
    {
        message.Context = _context;
        var worker = _serviceProvider.GetResolver<BaseHangfireWorker<I>>().Service;
        if (worker is null) throw new Exception("Message worker not found!");
        BackgroundJob.Enqueue(() => worker.DoJobWithContext(message));
    }
}
