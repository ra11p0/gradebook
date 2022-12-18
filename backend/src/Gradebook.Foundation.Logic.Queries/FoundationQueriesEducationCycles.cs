using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public partial class FoundationQueries : IFoundationEducationCyclesQueries
{
    public async Task<ResponseWithStatus<IPagedList<Guid>>> GetClassesGuidsForEducationCycle(Guid educationCycleGuid, int page, string? query)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetClassesGuidsForEducationCycle(educationCycleGuid, query, pager);
        return new ResponseWithStatus<IPagedList<Guid>>(resp);
    }
    public async Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesForEducationCycle(Guid educationCycle, int page, string? query)
    {
        var classesGuids = await GetClassesGuidsForEducationCycle(educationCycle, page, query);
        if (!classesGuids.Status) return new ResponseWithStatus<IPagedList<ClassDto>>(classesGuids.StatusCode);
        var classes = await Repository.GetClassesByGuids(classesGuids.Response!);
        return new ResponseWithStatus<IPagedList<ClassDto>>(classes.ToPagedList(classesGuids.Response!));
    }
    public async Task<ResponseWithStatus<IPagedList<EducationCycleDto>>> GetEducationCyclesInSchool(Guid schoolGuid, int page, string query)
    {
        if (!await _foundationPermissionLogic.Service.CanSeeEducationCycles(schoolGuid)) return new ResponseWithStatus<IPagedList<EducationCycleDto>>(403);
        var pager = new Pager(page);
        var res = await Repository.GetEducationCyclesInSchool(schoolGuid, pager, query);
        var resWithCreator = await Task.WhenAll(res.Select(async cycle =>
        {
            cycle.Creator = (await GetPersonByGuid(cycle.CreatorGuid)).Response;
            return cycle;
        }));
        return new ResponseWithStatus<IPagedList<EducationCycleDto>>(resWithCreator.ToPagedList(res));
    }
    public async Task<ResponseWithStatus<IPagedList<ClassDto>>> GetAvalibleClassesInForEducationCycle(Guid educationCycleGuid, int page, string? query)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetAvailableClassesWithAssignedForEducationCycle(educationCycleGuid, pager, query);
        if (resp is null) return new ResponseWithStatus<IPagedList<ClassDto>>(404);
        return new ResponseWithStatus<IPagedList<ClassDto>>(resp, true);
    }
}
