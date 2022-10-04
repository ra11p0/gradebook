using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Permissions.Logic.Queries;

public interface IPermissionsQueriesRepository : IBaseRepository
{
    Task<Dictionary<PermissionEnum, PermissionLevelEnum>> GetPermissionsForPerson(Guid personGuid);
    Task<PermissionLevelEnum> GetPermissionForPerson(Guid personGuid, PermissionEnum permissionId);
}
