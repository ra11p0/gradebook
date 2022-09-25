using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Commands.Definitions;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommands : BaseLogic<ISettingsCommandsRepository>, ISettingsCommands
{
    public SettingsCommands(ISettingsCommandsRepository repository) : base(repository)
    {
    }

    public async Task SetDefaultPersonGuid(string userGuid, Guid defaultPersonGuid)
    {
        await Repository.SetSettingForUserAsync(userGuid, SettingEnum.DefaultPersonGuid, defaultPersonGuid);
        await Repository.SaveChangesAsync();
    }

    public async Task<StatusResponse> SetAccountSettings(string userGuid, SettingsCommand settings)
    {
        if (settings.DefaultPersonGuid.HasValue) await SetDefaultPersonGuid(userGuid, settings.DefaultPersonGuid.Value);
        return new StatusResponse(true);
    }
}
