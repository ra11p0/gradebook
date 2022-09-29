using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Foundation.Common.Permissions.Commands;

public interface IPermissionsCommands
{
    Task SetPermissionsForPerson(Guid personGuid, Dictionary<PermissionEnum, PermissionLevelEnum> permissions);
}
