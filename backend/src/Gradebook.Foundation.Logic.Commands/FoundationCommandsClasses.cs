using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

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
    public async Task<StatusResponse> ConfigureEducationCycleForClass(Guid classGuid, EducationCycleConfigurationCommand configuration)
    {
        var currentPersonGuid = await _foundationQueries.Service.RecogniseCurrentPersonByClassGuid(classGuid);
        if (!currentPersonGuid.Status) return new StatusResponse(currentPersonGuid.StatusCode, currentPersonGuid.Message);
        var resp = await Repository.ConfigureEducationCycleForClass(classGuid, currentPersonGuid.Response, configuration);
        if (!resp.Status) return resp;
        await Repository.SaveChangesAsync();
        return resp;
    }
}
