namespace Gradebook.Foundation.Common.Permissions.Enums;

public enum PermissionLevelEnum
{
    Invitations_CannotInvite = 1,
    Invitations_CanInvite = 2,

    Permissions_CannotManagePermissions = 3,
    Permissions_CanManagePermissions = 4,

    Classes_ViewOnly = 5,
    Classes_CanManageOwn = 6,
    Classes_CanManageAll = 7,

    Subjects_ViewOnly = 8,
    Subjects_CanManageAssigned = 9,
    Subjects_CanManageAll = 10,

    Students_ViewOnly = 11,
    Students_CanCreateAndDelete = 12,
}
