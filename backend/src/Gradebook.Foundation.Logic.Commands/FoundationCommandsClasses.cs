using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;

namespace Gradebook.Foundation.Logic.Commands;

public partial class FoundationCommands
{
    public Task<StatusResponse> SetActiveEducationCycleToClass(Guid classGuid, Guid educationCycleGuid)
        => SetActiveEducationCycleToClasses(classGuid.AsEnumerable(), educationCycleGuid);
    public async Task<StatusResponse> SetActiveEducationCycleToClasses(IEnumerable<Guid> classesGuid, Guid educationCycleGuid)
    {
        var resp = await Repository.SetActiveEducationCycleToClasses(classesGuid, educationCycleGuid);
        await Repository.SaveChangesAsync();
        return resp;
    }
    public async Task<StatusResponse> DeleteActiveEducationCycleFromClasses(IEnumerable<Guid> classesGuids)
    {
        var resp = await Repository.DeleteActiveEducationCycleFromClasses(classesGuids);
        await Repository.SaveChangesAsync();
        return resp;
    }
    public Task<StatusResponse> DeleteActiveEducationCycleFromClass(Guid classGuid)
        => DeleteActiveEducationCycleFromClasses(classGuid.AsEnumerable());
}
