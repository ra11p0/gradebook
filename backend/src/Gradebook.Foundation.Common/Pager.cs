namespace Gradebook.Foundation.Common;

public class Pager : IPager
{
    private readonly int _page;
    private readonly int _pageSize;
    public int Page => _page;
    public int PageSize => _pageSize;
    public Pager(int page = 1, int pageSize = 20)
    {
        _page = page;
        _pageSize = pageSize;
    }
}
