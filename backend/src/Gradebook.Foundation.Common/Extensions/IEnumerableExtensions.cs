namespace Gradebook.Foundation.Common.Extensions;

public static class IEnumerableExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> list, int page, int total, int totalPages)
        => new PagedList<T>(page, total, totalPages, list);   
}
