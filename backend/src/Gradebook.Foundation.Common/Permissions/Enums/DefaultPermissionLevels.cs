using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Permissions.Enums;

public static class DefaultPermissionLevels
{
    public static Dictionary<PermissionEnum, PermissionLevelEnum> GetDefaultPermissionLevels(SchoolRoleEnum schoolRole)
    {
        return ((PermissionEnum[])Enum.GetValues(typeof(PermissionEnum))).ToDictionary(e => e, e => GetDefaultPermissionLevel(e, schoolRole));
    }
    public static PermissionLevelEnum GetDefaultPermissionLevel(PermissionEnum permission, SchoolRoleEnum schoolRole)
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
}
