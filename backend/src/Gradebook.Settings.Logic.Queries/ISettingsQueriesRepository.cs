using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Queries;

public interface ISettingsQueriesRepository : IBaseRepository
{
    Task<T?> GetSettingForPersonAsync<T>(Guid personGuid, SettingEnum settingType);
    T? GetSettingForPerson<T>(Guid personGuid, SettingEnum settingType);
}
