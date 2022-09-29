using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Commands;
using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Permissions.Logic.Commands;

public class PermissionsCommands : BaseLogic<IPermissionsCommandsRepository>, IPermissionsCommands
{
    public PermissionsCommands(IPermissionsCommandsRepository repository) : base(repository)
    {
    }

    public async Task SetPermissionsForPerson(Guid personGuid, Dictionary<PermissionEnum, PermissionLevelEnum> permissions)
    {
        await Repository.SetPermissionsForPerson(personGuid, permissions);
        await Repository.SaveChangesAsync();
    }
}
