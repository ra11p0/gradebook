using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Commands;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommands : BaseLogic<ISettingsCommandsRepository>, ISettingsCommands
{
    public SettingsCommands(ISettingsCommandsRepository repository) : base(repository)
    {
    }
}
