using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Logic.Commands;

public partial class FoundationCommands
{
    public async Task<StatusResponse> EditClassesAssignedToEducationCycle(IEnumerable<Guid> classesGuids, Guid educationCycleGuid)
    {
        Repository.BeginTransaction();
        var currentAssignedClasses = await _foundationQueries.Service.GetClassesGuidsForEducationCycle(educationCycleGuid, 0);
        if (!currentAssignedClasses.Status) return new StatusResponse(currentAssignedClasses.StatusCode);


        await Repository.SaveChangesAsync();
        Repository.CommitTransaction();
        return new StatusResponse(200);
    }
}
