using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Permissions.Enums;

public static class DefaultPermissionLevels
{
    public static IEnumerable<Tuple<PermissionEnum, PermissionLevelEnum>> GetDefaultPermissionLevels(SchoolRoleEnum schoolRole)
    {
        return ((PermissionEnum[])Enum.GetValues(typeof(PermissionEnum))).Select(permission => new Tuple<PermissionEnum, PermissionLevelEnum>(permission, GetDefaultPermissionLevel(permission, schoolRole)));
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
            _ => 0,
        };
    }
}
