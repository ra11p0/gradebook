using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Hangfire;
using Gradebook.Foundation.Hangfire;

namespace Gradebook.Foundation.Tests.Utils;

public class FakeHangfireClient : IHangfireClient
{
    private readonly Context _context;
    private readonly IServiceProvider _serviceProvider;
    public FakeHangfireClient(Context context, IServiceProvider provider)
    {
        _context = context;
        _serviceProvider = provider;
    }
    public void SendMessage<I>(I message) where I : BaseHangfireWorkerMessage
    {
        message.Context = _context;
        var worker = _serviceProvider.GetResolver<BaseHangfireWorker<I>>().Service;
        if (worker is null) throw new Exception("Message worker not found!");
        worker.DoJobWithContext(message).GetAwaiter();
    }
}
