using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Permissions.Logic.Queries;

public interface IPermissionsQueriesRepository : IBaseRepository
{
    Task<IEnumerable<Tuple<PermissionEnum, PermissionLevelEnum>>> GetPermissionsForPerson(Guid personGuid);
}
