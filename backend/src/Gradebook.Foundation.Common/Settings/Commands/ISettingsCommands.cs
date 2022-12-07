using Gradebook.Foundation.Common.Settings.Commands.Definitions;

namespace Gradebook.Foundation.Common.Settings.Commands;

public interface ISettingsCommands
{
    Task SetDefaultSchoolGuid(string userGuid, Guid defaultSchooluid);
    Task SetLanguage(string userGuid, string language);
    Task<StatusResponse> SetAccountSettings(SettingsCommand settings);
}
