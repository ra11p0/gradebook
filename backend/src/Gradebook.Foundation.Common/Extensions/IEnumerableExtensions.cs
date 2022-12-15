namespace Gradebook.Foundation.Common.Extensions;

public static class IEnumerableExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> list, int page, int total, int totalPages)
        => new PagedList<T>(page, total, totalPages, list);
    public static IPagedList<T> ToPagedList<T, _>(this IEnumerable<T> list, IPagedList<_> dataList)
        => new PagedList<T>(dataList.Page, dataList.Total, dataList.TotalPages, list);
}
