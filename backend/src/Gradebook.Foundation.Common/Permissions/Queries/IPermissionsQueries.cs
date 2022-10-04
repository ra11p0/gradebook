using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Foundation.Common.Permissions.Queries;

public interface IPermissionsQueries
{
    PermissionLevelEnum GetDefaultPermissionLevel(PermissionEnum permission, SchoolRoleEnum schoolRole);
    PermissionGroupEnum GetPermissionGroupForPermission(PermissionEnum permission);
    PermissionLevelEnum[] GetPermissionLevelsForPermission(PermissionEnum permission);
    PermissionEnum[] GetPermissionsInGroup(PermissionGroupEnum permissionGroup);
    Dictionary<PermissionEnum, PermissionLevelEnum> GetDefaultPermissionLevels(SchoolRoleEnum schoolRole);
    Task<Dictionary<PermissionEnum, PermissionLevelEnum>> GetPermissionsForPerson(Guid personGuid);
    Task<PermissionLevelEnum> GetPermissionForPerson(Guid personGuid, PermissionEnum permissionId);
}
