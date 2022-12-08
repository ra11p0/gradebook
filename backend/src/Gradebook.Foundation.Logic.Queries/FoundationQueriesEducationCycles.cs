using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Logic.Queries;

public partial class FoundationQueries
{
    public async Task<ResponseWithStatus<IPagedList<Guid>>> GetClassesGuidsForEducationCycle(Guid educationCycleGuid, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetClassesGuidsForEducationCycle(educationCycleGuid, pager);
        return new ResponseWithStatus<IPagedList<Guid>>(resp);
    }
}
