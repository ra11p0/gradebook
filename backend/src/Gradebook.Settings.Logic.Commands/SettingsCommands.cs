using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommands : BaseLogic<ISettingsCommandsRepository>, ISettingsCommands
{
    public SettingsCommands(ISettingsCommandsRepository repository) : base(repository)
    {
    }

    public async Task SetDefaultPersonGuid(Guid personGuid, Guid defaultPersonGuid)
    {
        await Repository.SetSettingForPersonAsync(personGuid, SettingEnum.DefaultPersonGuid, defaultPersonGuid);
        await Repository.SaveChangesAsync();
    }
}
