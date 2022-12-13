using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Logic.Commands;

public partial class FoundationCommands
{
    public async Task<ResponseWithStatus<Guid>> AddNewEducationCycle(EducationCycleCommand command)
    {
        if (!command.IsValid) return new ResponseWithStatus<Guid>("Invalid command");
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid(command.SchoolGuid);
        if (!currentPersonGuid.Status) return new ResponseWithStatus<Guid>(404, "Person not found");
        if (!await _foundationPermissions.Service.CanCreateEducationCycle(command.SchoolGuid)) return new ResponseWithStatus<Guid>(403);
        command.CreatedDate = Time.UtcNow;
        command.CreatorGuid = currentPersonGuid.Response;
        var res = await Repository.AddNewEducationCycle(command);
        if (!res.Status) return new ResponseWithStatus<Guid>(res.Message);
        await Repository.SaveChangesAsync();
        return new ResponseWithStatus<Guid>(res.Response);
    }
    public async Task<StatusResponse> EditClassesAssignedToEducationCycle(IEnumerable<Guid> classesGuids, Guid educationCycleGuid)
    {
        foreach (var classGuid in classesGuids)
        {
            if (!await _foundationPermissions.Service.CanManageClass(classGuid))
                return new StatusResponse(403);
        }
        Repository.BeginTransaction();
        var currentAssignedClasses = await _foundationQueries.Service.GetClassesGuidsForEducationCycle(educationCycleGuid, 0);
        if (!currentAssignedClasses.Status) return new StatusResponse(currentAssignedClasses.StatusCode);

        var classesToAdd = classesGuids.Where(e => !currentAssignedClasses.Response!.Contains(e));
        var classesToRemove = currentAssignedClasses.Response!.Where(e => !classesGuids.Contains(e));

        await Repository.SetActiveEducationCycleToClasses(classesToAdd, educationCycleGuid);
        await Repository.DeleteActiveEducationCycleFromClasses(classesToRemove);

        await Repository.SaveChangesAsync();
        Repository.CommitTransaction();
        return new StatusResponse(200);
    }
}
