using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Foundation.Common.Permissions.Queries;

public interface IPermissionsQueries
{
    Task<Dictionary<PermissionEnum, PermissionLevelEnum>> GetPermissionsForPerson(Guid personGuid);
}
