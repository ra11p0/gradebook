using Gradebook.Foundation.Common;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Logic.Commands.Repositories;

public partial class FoundationCommandsRepository
{
    public async Task<StatusResponse> SetActiveEducationCycleToClasses(IEnumerable<Guid> classesGuids, Guid educationCycleGuid)
    {
        var educationCycle = await Context.EducationCycles!.FirstOrDefaultAsync(e => e.Guid == educationCycleGuid);
        if (educationCycle is null) return new StatusResponse(404);
        foreach (var classGuid in classesGuids)
        {
            var _class = await Context.Classes!.FirstOrDefaultAsync(e => e.Guid == classGuid);
            if (_class is null) continue;
            _class.ActiveEducationCycle = educationCycle;
        }
        return new StatusResponse(200);
    }
    public async Task<StatusResponse> DeleteActiveEducationCycleFromClasses(IEnumerable<Guid> classesGuids)
    {
        foreach (var classGuid in classesGuids)
        {
            var _class = await Context.Classes!.FirstOrDefaultAsync(e => e.Guid == classGuid);
            if (_class is null) continue;
            _class.ActiveEducationCycle = null;
        }
        return new StatusResponse(200);
    }
}
