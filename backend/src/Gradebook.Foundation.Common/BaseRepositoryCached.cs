namespace Gradebook.Foundation.Common;

public class BaseRepositoryCached<T, C> where T: class, IBaseRepository
{
    protected T Base { get; }
    protected C CacheMachine { get; }
    public BaseRepositoryCached(T _base, C cacheMachine)
    {
        Base = _base;
        CacheMachine = cacheMachine;
    }
}
