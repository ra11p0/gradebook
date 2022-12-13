using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Logic.Commands;

public partial class FoundationCommands
{
    private async Task<StatusResponse> SetStudentsActiveClass(Guid classGuid, List<Guid> studentGuid)
    {
        foreach (var guid in studentGuid) if (!(await Repository.SetStudentActiveClass(classGuid, guid)).Status) return new StatusResponse(false, "Could not set active school");
        return new StatusResponse(true);
    }
    private async Task<StatusResponse> RemoveStudentsActiveClass(List<Guid> studentGuid)
    {
        foreach (var guid in studentGuid) if (!(await Repository.RemoveStudentActiveClass(guid)).Status) return new StatusResponse(false, "Could not remove active school");
        return new StatusResponse(true);
    }
    public async Task<StatusResponse> SetActiveEducationCycleToClass(Guid classGuid, Guid educationCycleGuid)
    {
        if (!await _foundationPermissions.Service.CanManageClass(classGuid))
            return new StatusResponse(403);
        return await SetActiveEducationCycleToClasses(classGuid.AsEnumerable(), educationCycleGuid);
    }
    public async Task<StatusResponse> SetActiveEducationCycleToClasses(IEnumerable<Guid> classesGuids, Guid educationCycleGuid)
    {
        var anyWithoutPermission = (await Task.WhenAll(classesGuids.Select(e => _foundationPermissions.Service.CanManageClass(e)))).Any(e => !e);
        if (anyWithoutPermission)
            return new StatusResponse(403);
        var resp = await Repository.SetActiveEducationCycleToClasses(classesGuids, educationCycleGuid);
        await Repository.SaveChangesAsync();
        return resp;
    }
    public async Task<StatusResponse> DeleteActiveEducationCycleFromClasses(IEnumerable<Guid> classesGuids)
    {
        var anyWithoutPermission = (await Task.WhenAll(classesGuids.Select(e => _foundationPermissions.Service.CanManageClass(e)))).Any(e => !e);
        if (anyWithoutPermission)
            return new StatusResponse(403);
        var resp = await Repository.DeleteActiveEducationCycleFromClasses(classesGuids);
        await Repository.SaveChangesAsync();
        return resp;
    }
    public async Task<StatusResponse> DeleteActiveEducationCycleFromClass(Guid classGuid)
    {
        if (!await _foundationPermissions.Service.CanManageClass(classGuid))
            return new StatusResponse(403);
        return await DeleteActiveEducationCycleFromClasses(classGuid.AsEnumerable());
    }
    public async Task<StatusResponse> ConfigureEducationCycleForClass(Guid classGuid, EducationCycleConfigurationCommand configuration)
    {
        if (!configuration.IsValid) return new StatusResponse(400);
        var currentPersonGuid = await _foundationQueries.Service.RecogniseCurrentPersonByClassGuid(classGuid);
        if (!currentPersonGuid.Status) return new StatusResponse(currentPersonGuid.StatusCode, currentPersonGuid.Message);
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPersonGuid.Response))
            return new StatusResponse(403);
        var resp = await Repository.ConfigureEducationCycleForClass(classGuid, currentPersonGuid.Response, configuration);
        if (!resp.Status) return resp;
        await Repository.SaveChangesAsync();
        return resp;
    }
    public async Task<StatusResponse> DeleteClass(Guid classGuid)
    {
        var person = await _foundationQueries.Service.RecogniseCurrentPersonByClassGuid(classGuid);
        if (!person.Status) return new StatusResponse(person.Message);
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, person.Response))
            return new StatusResponse(403);
        var resp = await Repository.DeleteClass(classGuid);
        if (!resp.Status) return new StatusResponse(false, resp.Message);
        await Repository.SaveChangesAsync();
        return resp;
    }
}
