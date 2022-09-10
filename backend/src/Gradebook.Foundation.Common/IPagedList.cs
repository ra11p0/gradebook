using System.Collections;

namespace Gradebook.Foundation.Common;

public interface IPagedList<T> : IEnumerable<T>
{
    public int Page { get; }
    public int Total { get; }
    public int TotalPages { get; }
}
