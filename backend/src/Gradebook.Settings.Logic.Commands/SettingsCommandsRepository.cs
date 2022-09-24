using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;
using Gradebook.Settings.Database;
using Newtonsoft.Json;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommandsRepository : BaseRepository<SettingsDatabaseContext>, ISettingsCommandsRepository
{
    public SettingsCommandsRepository(SettingsDatabaseContext context) : base(context)
    {
    }

    public void SetSettingForPerson<T>(Guid personGuid, SettingEnum settingType, T value)
        => SetSettingForPersonAsync(personGuid, settingType, value).GetAwaiter().GetResult();

    public async Task SetSettingForPersonAsync<T>(Guid personGuid, SettingEnum settingType, T value)
    {
        var setting = Context.Settings!.FirstOrDefault(setting => setting.PersonGuid == personGuid && setting.SettingType == settingType);
        if (setting is null)
        {
            setting = new()
            {
                PersonGuid = personGuid,
                SettingType = settingType,
                JsonValue = JsonConvert.SerializeObject(value)
            };
            await Context.Settings!.AddAsync(setting);
        }
        else
        {
            setting.JsonValue = JsonConvert.SerializeObject(value);
        }
    }
}
