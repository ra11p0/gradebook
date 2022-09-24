using Dapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;
using Gradebook.Settings.Database;
using Newtonsoft.Json;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueriesRepository : BaseRepository<SettingsDatabaseContext>, ISettingsQueriesRepository
{
    public SettingsQueriesRepository(SettingsDatabaseContext context) : base(context)
    {
    }

    public T? GetSettingForPerson<T>(Guid personGuid, SettingEnum settingType)
        => GetSettingForPersonAsync<T>(personGuid, settingType).Result;

    public async Task<T?> GetSettingForPersonAsync<T>(Guid personGuid, SettingEnum settingType)
    {
        using var cn = await GetOpenConnectionAsync();
        var jsonString = await cn.QuerySingleOrDefaultAsync<string>(@"
            SELECT JsonValue 
            FROM Settings
            WHERE PersonGuid = @personGuid AND SettingType = @settingType
        ", new { personGuid, settingType = (int)settingType });
        return jsonString is null ? default : JsonConvert.DeserializeObject<T?>(jsonString);
    }

    public T? GetSettingForUser<T>(string userGuid, SettingEnum settingType)
        => GetSettingForUserAsync<T>(userGuid, settingType).Result;

    public async Task<T?> GetSettingForUserAsync<T>(string userGuid, SettingEnum settingType)
    {
        using var cn = await GetOpenConnectionAsync();
        var jsonString = await cn.QuerySingleOrDefaultAsync<string>(@"
            SELECT JsonValue 
            FROM AccountSettings
            WHERE UserGuid = @userGuid AND SettingType = @settingType
        ", new { userGuid, settingType = (int)settingType });
        return jsonString is null ? default : JsonConvert.DeserializeObject<T?>(jsonString);
    }
}
