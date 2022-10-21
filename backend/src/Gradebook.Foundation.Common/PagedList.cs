namespace Gradebook.Foundation.Common;

public class PagedList<T> : List<T>, IPagedList<T>
{
    private readonly int _page;
    private readonly int _total;
    private readonly int _totalPages;
    public int Page => _page;
    public int Total => _total;
    public int TotalPages => _totalPages;
    public PagedList(int page, int total, int totalPages, IEnumerable<T> list)
    {
        _page = page;
        _total = total;
        _totalPages = totalPages;
        AddRange(list);
    }
    public PagedList(IEnumerable<T>? list = null)
    {
        _page = -1;
        _total = -1;
        _totalPages = -1;
        if (list is not null) AddRange(list);
    }
}
