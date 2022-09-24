using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Commands;

public interface ISettingsCommandsRepository : IBaseRepository
{
    Task SetSettingForPersonAsync<T>(Guid personGuid, SettingEnum settingType, T value);
    void SetSettingForPerson<T>(Guid personGuid, SettingEnum settingType, T value);
}
