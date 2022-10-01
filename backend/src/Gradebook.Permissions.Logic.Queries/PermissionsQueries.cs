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

    public PermissionGroupEnum GetPermissionGroupForPermission(PermissionEnum permission)
    {
        return permission switch
        {
            PermissionEnum.Invitations => PermissionGroupEnum.Administration,
            PermissionEnum.Permissions => PermissionGroupEnum.System,
            _ => 0,
        };
    }

    public PermissionLevelEnum[] GetPermissionLevelsForPermission(PermissionEnum permission)
    {
        return permission switch
        {
            PermissionEnum.Invitations => new PermissionLevelEnum[] { PermissionLevelEnum.Invitations_CannotInvite, PermissionLevelEnum.Invitations_CanInvite },
            PermissionEnum.Permissions => new PermissionLevelEnum[] { PermissionLevelEnum.Permissions_CannotManagePermissions, PermissionLevelEnum.Permissions_CanManagePermissions },
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
