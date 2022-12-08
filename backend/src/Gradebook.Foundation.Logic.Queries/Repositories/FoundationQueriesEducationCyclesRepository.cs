using DbExtensions;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public partial class FoundationQueriesRepository
{
    public async Task<IPagedList<Guid>> GetClassesGuidsForEducationCycle(Guid educationCycleGuid, Pager pager)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Guid");
        builder.FROM("EducationCycles");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("ActiveEducationCycleGuid = @educationCycleGuid");
        builder.ORDER_BY("CreatedDate");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<Guid>(builder.ToString(), new { educationCycleGuid }, pager);
    }
}
