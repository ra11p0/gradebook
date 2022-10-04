using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Permissions;
using Gradebook.Foundation.Common.Permissions.Queries;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsPermissionsLogic : IPermissionsPermissionsLogic
{
    private readonly ServiceResolver<IPermissionsQueries> _permissionsQueries;
    public PermissionsPermissionsLogic(IServiceProvider serviceProvider)
    {
        _permissionsQueries = serviceProvider.GetResolver<IPermissionsQueries>();
    }
    public async Task<bool> CanManagePermissions(Guid personGuid)
    {
        var permissionLevel = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Foundation.Common.Permissions.Enums.PermissionEnum.Permissions);
        return permissionLevel == Foundation.Common.Permissions.Enums.PermissionLevelEnum.Permissions_CanManagePermissions;
    }
}
