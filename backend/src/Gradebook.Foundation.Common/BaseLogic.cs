namespace Gradebook.Foundation.Common;

public abstract class BaseLogic<T> where T : IBaseRepository
{
    protected T Repository { get; }

    protected BaseLogic(T repository)
    {
        Repository = repository;
    }
}
