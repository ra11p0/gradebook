using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Logic.Commands;

public partial class FoundationCommands
{
    public async Task<StatusResponse> EditClassesAssignedToEducationCycle(IEnumerable<Guid> classesGuids, Guid educationCycleGuid)
    {
        Repository.BeginTransaction();

        await Repository.SaveChangesAsync();
        Repository.CommitTransaction();
        return new StatusResponse(200);
    }
}
