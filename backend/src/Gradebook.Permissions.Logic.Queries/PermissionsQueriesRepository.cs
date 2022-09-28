using Dapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Permissions.Database;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueriesRepository : BaseRepository<PermissionsDatabaseContext>, IPermissionsQueriesRepository
{
    public PermissionsQueriesRepository(PermissionsDatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Tuple<PermissionEnum, PermissionLevelEnum>>> GetPermissionsForPerson(Guid personGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<Tuple<PermissionEnum, PermissionLevelEnum>>(@"
            SELECT PermissionId AS Item1, PermissionLevel AS Item2
            FROM Permissions
            WHERE PersonGuid = @personGuid
        ", new { personGuid });
    }
}
