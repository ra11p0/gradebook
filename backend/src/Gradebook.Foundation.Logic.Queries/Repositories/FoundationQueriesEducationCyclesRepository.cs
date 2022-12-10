using DbExtensions;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public partial class FoundationQueriesRepository
{
    public async Task<IPagedList<Guid>> GetClassesGuidsForEducationCycle(Guid educationCycleGuid, string? query, Pager pager)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Guid");
        builder.FROM("Classes");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("ActiveEducationCycleGuid = @educationCycleGuid");
        if (!string.IsNullOrEmpty(query))
            builder.WHERE("Name like @query");
        builder.ORDER_BY("CreatedDate");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<Guid>(builder.ToString(), new { educationCycleGuid, query }, pager);
    }
}
