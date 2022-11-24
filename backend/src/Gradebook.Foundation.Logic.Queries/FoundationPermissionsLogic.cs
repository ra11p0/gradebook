using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Permissions.Queries;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationPermissionsLogic : IFoundationPermissionsLogic
{
    private readonly ServiceResolver<IPermissionsQueries> _permissionsQueries;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public FoundationPermissionsLogic(IServiceProvider serviceProvider)
    {
        _permissionsQueries = serviceProvider.GetResolver<IPermissionsQueries>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }

    public async Task<bool> CanCreateEducationCycle(Guid schoolGuid)
    {
        var personGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!personGuid.Status) throw new Exception("Person does not exist");
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid.Response, Common.Permissions.Enums.PermissionEnum.EducationCycles);
        return permission is Common.Permissions.Enums.PermissionLevelEnum.EducationCycles_CanCreateAndDelete;
    }

    public async Task<bool> CanCreateNewClass(Guid personGuid)
    {
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Common.Permissions.Enums.PermissionEnum.Classes);
        return permission != Common.Permissions.Enums.PermissionLevelEnum.Classes_ViewOnly;
    }

    public async Task<bool> CanCreateNewStudents(Guid personGuid)
    {
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Common.Permissions.Enums.PermissionEnum.Students);
        return permission == Common.Permissions.Enums.PermissionLevelEnum.Students_CanCreateAndDelete;
    }

    public async Task<bool> CanCreateNewSubject(Guid personGuid)
    {
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Common.Permissions.Enums.PermissionEnum.Subjects);
        return permission == Common.Permissions.Enums.PermissionLevelEnum.Subjects_CanManageAll ||
            permission == Common.Permissions.Enums.PermissionLevelEnum.Subjects_CanManageAssigned;
    }

    public Task<bool> CanDeleteStudents(Guid personGuid)
        => CanCreateNewStudents(personGuid);

    public async Task<bool> CanInviteToSchool(Guid personGuid)
    {
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Common.Permissions.Enums.PermissionEnum.Invitations);
        return permission == Common.Permissions.Enums.PermissionLevelEnum.Invitations_CanInvite;
    }

    public async Task<bool> CanManageClass(Guid classGuid, Guid personGuid)
    {
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Common.Permissions.Enums.PermissionEnum.Classes);
        if (permission is Common.Permissions.Enums.PermissionLevelEnum.Classes_ViewOnly) return false;
        if (permission is Common.Permissions.Enums.PermissionLevelEnum.Classes_CanManageAll) return true;
        return (await _foundationQueries.Service.IsClassOwner(classGuid, personGuid)).Response;
    }

    public async Task<bool> CanManageSubject(Guid subjectGuid, Guid personGuid)
    {
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Common.Permissions.Enums.PermissionEnum.Subjects);
        if (permission == Common.Permissions.Enums.PermissionLevelEnum.Subjects_CanManageAll) return true;
        if (permission == Common.Permissions.Enums.PermissionLevelEnum.Subjects_ViewOnly) return false;
        var teachersForSubject = await _foundationQueries.Service.GetTeachersForSubject(subjectGuid, 0);
        if (!teachersForSubject.Status) return false;
        if (!teachersForSubject.Response!.Any()) return false;
        return teachersForSubject.Response!.Any(te => te.Guid == personGuid);
    }

    public async Task<bool> CanSeeEducationCycles(Guid schoolGuid)
    {
        var personGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!personGuid.Status) throw new Exception("Person does not exist");
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid.Response, Common.Permissions.Enums.PermissionEnum.EducationCycles);
        return permission is not Common.Permissions.Enums.PermissionLevelEnum.EducationCycles_NoAccess;
    }
}
