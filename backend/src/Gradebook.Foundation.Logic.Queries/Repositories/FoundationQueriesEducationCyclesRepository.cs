using Dapper;
using DbExtensions;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

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
    public async Task<IEnumerable<EducationCycleInstanceDto>> GetEducationCycleInstancesByGuids(IEnumerable<Guid> guids)
    {
        var builder = new SqlBuilder();
        builder.SELECT("eci.Guid, EducationCycleGuid, ec.Name as EducationCycleName, DateSince, DateUntil, eci.CreatorGuid");
        builder.FROM("EducationCycleInstances eci");
        builder.JOIN("EducationCycles ec ON ec.Guid = eci.EducationCycleGuid");
        builder.WHERE("eci.IsDeleted = 0");
        builder.WHERE("eci.Guid IN @guids");

        builder.ORDER_BY("DateSince");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<EducationCycleInstanceDto>(builder.ToString(), new { guids });
    }
    public async Task<IEnumerable<EducationCycleStepInstanceDto>> GetEducationCycleStepInstancesByEducationCycleInstancesGuids(IEnumerable<Guid> guids)
    {
        var builder = new SqlBuilder();
        builder.SELECT("DateSince, DateUntil, ecsi.Guid, `Order`, EducationCycleInstanceGuid");
        builder.FROM("EducationCycleStepInstances ecsi");
        builder.JOIN("EducationCycleSteps ecs ON ecs.Guid = ecsi.EducationCycleStepGuid");
        builder.WHERE("ecsi.IsDeleted = 0");
        builder.WHERE("ecsi.EducationCycleInstanceGuid IN @guids");

        builder.ORDER_BY("`Order`");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<EducationCycleStepInstanceDto>(builder.ToString(), new { guids });
    }
    public async Task<IEnumerable<EducationCycleStepSubjectInstanceDto>> GetEducationCycleStepSubjectInstancesByEducationCycleStepInstancesGuids(IEnumerable<Guid> guids)
    {
        var builder = new SqlBuilder();
        builder.SELECT(@"ecssi.Guid, 
            AssignedTeacherGuid, 
            p.Name as TeacherName, 
            p.Surname as TeacherLastName, 
            SubjectGuid, 
            s.Name as SubjectName,
            HoursInStep,
            IsMandatory,
            GroupsAllowed
            ");
        builder.FROM("EducationCycleStepSubjectInstances ecssi");
        builder.JOIN("EducationCycleStepInstances ecsi ON ecsi.Guid = ecssi.EducationCycleStepInstanceGuid");
        builder.JOIN("EducationCycleInstances eci ON eci.Guid = ecsi.EducationCycleInstanceGuid");
        builder.JOIN("EducationCycleStepSubjects ecss ON ecss.Guid = ecssi.EducationCycleStepSubjectGuid");
        builder.JOIN("Subjects s ON s.Guid = ecss.SubjectGuid");
        builder.JOIN("Person p ON p.Guid = ecssi.AssignedTeacherGuid");
        builder.WHERE("ecssi.IsDeleted = 0");
        builder.WHERE("ecssi.Guid IN @guids");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<EducationCycleStepSubjectInstanceDto>(builder.ToString(), new { guids });
    }
    public async Task<IPagedList<EducationCycleExtendedDto>> GetEducationCyclesByGuids(IEnumerable<Guid> guids, Pager pager)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Name, SchoolGuid, Guid, CreatedDate, CreatorGuid");
        builder.FROM("EducationCycles");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("Guid in @guids");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<EducationCycleExtendedDto>(builder.ToString(), new { guids }, pager);
    }
}
