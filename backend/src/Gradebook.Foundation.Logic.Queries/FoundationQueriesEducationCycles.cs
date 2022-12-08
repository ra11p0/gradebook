using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public partial class FoundationQueries
{
    public async Task<ResponseWithStatus<IPagedList<Guid>>> GetClassesGuidsForEducationCycle(Guid educationCycleGuid, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetClassesGuidsForEducationCycle(educationCycleGuid, pager);
        return new ResponseWithStatus<IPagedList<Guid>>(resp);
    }
    public async Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesForEducationCycle(Guid educationCycle, int page)
    {
        var classesGuids = await GetClassesGuidsForEducationCycle(educationCycle, page);
        if (!classesGuids.Status) return new ResponseWithStatus<IPagedList<ClassDto>>(classesGuids.StatusCode);
        var classes = await Repository.GetClassesByGuids(classesGuids.Response!);
        return new ResponseWithStatus<IPagedList<ClassDto>>(classes.ToPagedList(classesGuids.Response!));
    }
}
