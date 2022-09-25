using Gradebook.Foundation.Common.Settings.Queries.Definitions;

namespace Gradebook.Foundation.Common.Settings.Commands;

public interface ISettingsQueries
{
    Task<Guid> GetDefaultPersonGuid(string userGuid);
    Task<ResponseWithStatus<SettingsDto>> GetAccountSettings(string userGuid);
}
