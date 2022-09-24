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
            SELECT Value 
            FROM Settings
            WHERE PersonGuid = @personGuid AND settingType = @settingType
        ", new { personGuid, settingType = (int)settingType });
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}
