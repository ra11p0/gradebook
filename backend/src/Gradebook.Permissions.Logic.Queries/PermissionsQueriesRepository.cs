using Gradebook.Foundation.Common;
using Gradebook.Permissions.Database;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueriesRepository : BaseRepository<PermissionsDatabaseContext>, IPermissionsQueriesRepository
{
    public PermissionsQueriesRepository(PermissionsDatabaseContext context) : base(context)
    {
    }
}
