using Gradebook.Foundation.Common.Extensions;

namespace Gradebook.Foundation.Common;

public class ServiceResolver<T> where T: class
{
    private T? _service;
    private IServiceProvider _serviceProvider;
    public T Service => _service
        ?? (_service = _serviceProvider.Resolve<T>());
    public ServiceResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}
