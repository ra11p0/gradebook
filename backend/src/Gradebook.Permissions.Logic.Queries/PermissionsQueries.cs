using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Foundation.Common.Permissions.Queries;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueries : BaseLogic<IPermissionsQueriesRepository>, IPermissionsQueries
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public PermissionsQueries(IPermissionsQueriesRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }

    //  Default permission levels for each roles
    public PermissionLevelEnum GetDefaultPermissionLevel(PermissionEnum permission, SchoolRoleEnum schoolRole)
    {
        return permission switch
        {
            PermissionEnum.Invitations => schoolRole switch
            {
                SchoolRoleEnum.Student => PermissionLevelEnum.Invitations_CannotInvite,
                SchoolRoleEnum.Teacher => PermissionLevelEnum.Invitations_CannotInvite,
                SchoolRoleEnum.Admin => PermissionLevelEnum.Invitations_CanInvite,
                _ => 0,
            },
            PermissionEnum.Permissions => schoolRole switch
            {
                SchoolRoleEnum.Student => PermissionLevelEnum.Permissions_CannotManagePermissions,
                SchoolRoleEnum.Teacher => PermissionLevelEnum.Permissions_CannotManagePermissions,
                SchoolRoleEnum.Admin => PermissionLevelEnum.Permissions_CanManagePermissions,
                _ => 0,
            },
            PermissionEnum.Classes => schoolRole switch
            {
                SchoolRoleEnum.Student => PermissionLevelEnum.Classes_ViewOnly,
                SchoolRoleEnum.Teacher => PermissionLevelEnum.Classes_CanManageOwn,
                SchoolRoleEnum.Admin => PermissionLevelEnum.Classes_CanManageAll,
                _ => 0,
            },
            PermissionEnum.Subjects => schoolRole switch
            {
                SchoolRoleEnum.Student => PermissionLevelEnum.Subjects_ViewOnly,
                SchoolRoleEnum.Teacher => PermissionLevelEnum.Subjects_CanManageAssigned,
                SchoolRoleEnum.Admin => PermissionLevelEnum.Subjects_CanManageAll,
                _ => 0,
            },
            PermissionEnum.Students => schoolRole switch
            {
                SchoolRoleEnum.Student => PermissionLevelEnum.Students_ViewOnly,
                SchoolRoleEnum.Teacher => PermissionLevelEnum.Students_CanCreateAndDelete,
                SchoolRoleEnum.Admin => PermissionLevelEnum.Students_CanCreateAndDelete,
                _ => 0,
            },
            PermissionEnum.EducationCycles => schoolRole switch
            {
                SchoolRoleEnum.Student => PermissionLevelEnum.EducationCycles_NoAccess,
                SchoolRoleEnum.Teacher => PermissionLevelEnum.EducationCycles_ViewOnly,
                SchoolRoleEnum.Admin => PermissionLevelEnum.EducationCycles_CanCreateAndDelete,
                _ => 0,
            },
            _ => 0,
        };
    }

    public Dictionary<PermissionEnum, PermissionLevelEnum> GetDefaultPermissionLevels(SchoolRoleEnum schoolRole)
    {
        return ((PermissionEnum[])Enum.GetValues(typeof(PermissionEnum))).ToDictionary(e => e, e => GetDefaultPermissionLevel(e, schoolRole));
    }

    public async Task<PermissionLevelEnum> GetPermissionForPerson(Guid personGuid, PermissionEnum permissionId)
    {
        return (await GetPermissionsForPerson(personGuid))[permissionId];
    }

    //  Permissions in permission group
    public PermissionGroupEnum GetPermissionGroupForPermission(PermissionEnum permission)
    {
        return permission switch
        {
            PermissionEnum.Classes => PermissionGroupEnum.Administration,
            PermissionEnum.Invitations => PermissionGroupEnum.Administration,
            PermissionEnum.Subjects => PermissionGroupEnum.Administration,
            PermissionEnum.Students => PermissionGroupEnum.Administration,
            PermissionEnum.EducationCycles => PermissionGroupEnum.Administration,
            PermissionEnum.Permissions => PermissionGroupEnum.System,
            _ => 0,
        };
    }

    //  Permission levels for permissions (lowest -> highest)
    public PermissionLevelEnum[] GetPermissionLevelsForPermission(PermissionEnum permission)
    {
        return permission switch
        {
            PermissionEnum.Invitations => new PermissionLevelEnum[] {
                PermissionLevelEnum.Invitations_CannotInvite,
                PermissionLevelEnum.Invitations_CanInvite
                },
            PermissionEnum.Permissions => new PermissionLevelEnum[] {
                PermissionLevelEnum.Permissions_CannotManagePermissions,
                PermissionLevelEnum.Permissions_CanManagePermissions
                },
            PermissionEnum.Students => new PermissionLevelEnum[] {
                PermissionLevelEnum.Students_ViewOnly,
                PermissionLevelEnum.Students_CanCreateAndDelete
                },
            PermissionEnum.Classes => new PermissionLevelEnum[] {
                PermissionLevelEnum.Classes_ViewOnly,
                PermissionLevelEnum.Classes_CanManageOwn,
                PermissionLevelEnum.Classes_CanManageAll
                },
            PermissionEnum.Subjects => new PermissionLevelEnum[] {
                PermissionLevelEnum.Subjects_ViewOnly,
                PermissionLevelEnum.Subjects_CanManageAssigned,
                PermissionLevelEnum.Subjects_CanManageAll
                },
            PermissionEnum.EducationCycles => new PermissionLevelEnum[] {
                PermissionLevelEnum.EducationCycles_NoAccess,
                PermissionLevelEnum.EducationCycles_ViewOnly,
                PermissionLevelEnum.EducationCycles_CanCreateAndDelete
                },
            _ => Array.Empty<PermissionLevelEnum>()
        };
    }

    public async Task<Dictionary<PermissionEnum, PermissionLevelEnum>> GetPermissionsForPerson(Guid personGuid)
    {
        var personResponse = await _foundationQueries.Service.GetPersonByGuid(personGuid);
        if (!personResponse.Status) throw new Exception("Could not get person!");
        var permissions = await Repository.GetPermissionsForPerson(personGuid);
        Dictionary<PermissionEnum, PermissionLevelEnum> permissionsWithDefaults = new();
        foreach (var permission in GetDefaultPermissionLevels(personResponse.Response!.SchoolRole))
            permissionsWithDefaults[permission.Key] = permissions.ContainsKey(permission.Key) ? permissions[permission.Key] : permission.Value;
        return permissionsWithDefaults;
    }

    public PermissionEnum[] GetPermissionsInGroup(PermissionGroupEnum permissionGroup)
    {
        var allPermissions = (PermissionEnum[])Enum.GetValues(typeof(PermissionEnum));
        return allPermissions.Where(p => GetPermissionGroupForPermission(p) == permissionGroup).ToArray();
    }
}
