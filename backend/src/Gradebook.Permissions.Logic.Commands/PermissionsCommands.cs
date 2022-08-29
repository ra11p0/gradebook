using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Commands;

namespace Gradebook.Permissions.Logic.Commands;

public class PermissionsCommands : BaseLogic<IPermissionsCommandsRepository>, IPermissionsCommands
{
    public PermissionsCommands(IPermissionsCommandsRepository repository) : base(repository)
    {
    }
}
