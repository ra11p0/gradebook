using Dapper;
using DbExtensions;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public partial class FoundationQueriesRepository
{
    public async Task<ClassDto?> GetClassByGuid(Guid guid)
        => (await GetClassesByGuids(guid.AsEnumerable())).FirstOrDefault();
    public async Task<IEnumerable<ClassDto>> GetClassesByGuids(IEnumerable<Guid> guids)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<ClassDto>(@"
                SELECT Name, CreatedDate, Description, Guid, SchoolGuid
                FROM Classes
                WHERE Guid IN @guids
                    AND IsDeleted = 0
            ", new
        {
            guids
        });
    }
    public async Task<IPagedList<EducationCycleInstanceDto>> GetEducationCycleInstancesByClassGuid(Guid classGuid, Pager pager)
    {
        var builder = new SqlBuilder();

        builder.SELECT("");
        builder.FROM("");
        builder.WHERE("");

        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<EducationCycleInstanceDto>(builder.ToString(), new { classGuid }, pager);
    }
    public async Task<Guid?> GetActiveEducationCycleGuidByClassGuid(Guid classGuid)
    {
        var builder = new SqlBuilder();

        builder.SELECT("ActiveEducationCycleGuid");
        builder.FROM("Classes");
        builder.WHERE("Guid = @classGuid");

        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<Guid?>(builder.ToString(), new { classGuid });
    }
    public async Task<IPagedList<Guid>> GetEducationCycleInstancesGuidsByClassGuid(Guid classGuid, Pager pager)
    {
        var builder = new SqlBuilder();

        builder.SELECT("Guid");
        builder.FROM("Classes");
        builder.WHERE("Guid = @classGuid");
        builder.WHERE("IsDeleted = 0");

        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<Guid>(builder.ToString(), new { classGuid }, pager);
    }
    public async Task<Guid?> GetEducationCycleInstanceForClass(Guid classGuid, Guid educationCycleGuid)
    {
        var builder = new SqlBuilder();

        builder.SELECT("Guid");
        builder.FROM("EducationCycleInstances");
        builder.WHERE("ClassGuid = @classGuid");
        builder.WHERE("EducationCycleGuid = @educationCycleGuid");

        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<Guid?>(builder.ToString(), new { classGuid, educationCycleGuid });
    }
}
