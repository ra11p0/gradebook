using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Permissions.Queries;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationPermissionsLogic : IFoundationPermissionsLogic
{
    private readonly ServiceResolver<IPermissionsQueries> _permissionsQueries;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public FoundationPermissionsLogic(IServiceProvider serviceProvider)
    {
        _permissionsQueries = serviceProvider.GetResolver<IPermissionsQueries>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    public async Task<bool> CanInviteToSchool(Guid personGuid)
    {
        var permission = await _permissionsQueries.Service.GetPermissionForPerson(personGuid, Common.Permissions.Enums.PermissionEnum.Invitations);
        return permission == Common.Permissions.Enums.PermissionLevelEnum.Invitations_CanInvite;
    }
}
