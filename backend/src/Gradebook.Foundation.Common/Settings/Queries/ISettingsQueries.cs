using Gradebook.Foundation.Common.Settings.Queries.Definitions;

namespace Gradebook.Foundation.Common.Settings.Commands;

public interface ISettingsQueries
{
    Task<Guid> GetDefaultSchoolGuid(string userGuid);
    Task<ResponseWithStatus<SettingsDto>> GetAccountSettings();
    Task<string?> GetUserLanguage(string userGuid);
}
