using Gradebook.Foundation.Common.Settings.Commands.Definitions;

namespace Gradebook.Foundation.Common.Settings.Commands;

public interface ISettingsCommands
{
    Task SetDefaultPersonGuid(string userGuid, Guid defaultPersonGuid);
    Task SetLanguage(string userGuid, string language);
    Task<StatusResponse> SetAccountSettings(SettingsCommand settings);
}
