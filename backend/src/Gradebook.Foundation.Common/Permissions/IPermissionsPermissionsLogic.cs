namespace Gradebook.Foundation.Common.Permissions;

public interface IPermissionsPermissionsLogic
{
    Task<bool> CanManagePermissions(Guid personGuid);
}
