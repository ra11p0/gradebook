using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Permissions;
using Gradebook.Foundation.Common.Permissions.Commands;
using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Permissions.Logic.Commands;

public class PermissionsCommands : BaseLogic<IPermissionsCommandsRepository>, IPermissionsCommands
{

    private readonly ServiceResolver<IPermissionsPermissionsLogic> _permissionsLogic;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public PermissionsCommands(IPermissionsCommandsRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _permissionsLogic = serviceProvider.GetResolver<IPermissionsPermissionsLogic>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }

    public async Task SetPermissionsForPerson(Guid personGuid, Dictionary<PermissionEnum, PermissionLevelEnum> permissions)
    {
        var changingPersonGuid = await _foundationQueries.Service.RecogniseCurrentPersonByRelatedPerson(personGuid);
        if (!changingPersonGuid.Status) return;
        if (!await _permissionsLogic.Service.CanManagePermissions(changingPersonGuid.Response)) return;
        await Repository.SetPermissionsForPerson(personGuid, permissions);
        await Repository.SaveChangesAsync();
    }
}
