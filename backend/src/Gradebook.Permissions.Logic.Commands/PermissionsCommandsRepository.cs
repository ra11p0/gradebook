using Gradebook.Foundation.Common;
using Gradebook.Permissions.Database;

namespace Gradebook.Permissions.Logic.Commands;

public class PermissionsCommandsRepository : BaseRepository<PermissionsDatabaseContext>, IPermissionsCommandsRepository
{
    public PermissionsCommandsRepository(PermissionsDatabaseContext context) : base(context)
    {
    }
}
