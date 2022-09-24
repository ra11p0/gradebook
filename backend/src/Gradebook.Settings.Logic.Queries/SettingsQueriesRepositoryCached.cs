using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueriesRepositoryCached : BaseRepositoryCached<SettingsQueriesRepository, object>, ISettingsQueriesRepository
{
    public SettingsQueriesRepositoryCached(SettingsQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public void BeginTransaction() => Base.BeginTransaction();

    public void CommitTransaction() => Base.CommitTransaction();

    public T? GetSettingForPerson<T>(Guid personGuid, SettingEnum settingType)
        => Base.GetSettingForPerson<T>(personGuid, settingType);

    public Task<T?> GetSettingForPersonAsync<T>(Guid personGuid, SettingEnum settingType)
        => Base.GetSettingForPersonAsync<T>(personGuid, settingType);

    public T? GetSettingForUser<T>(string userGuid, SettingEnum settingType) => GetSettingForUser<T>(userGuid, settingType);

    public Task<T?> GetSettingForUserAsync<T>(string userGuid, SettingEnum settingType) => Base.GetSettingForUserAsync<T>(userGuid, settingType);

    public void RollbackTransaction() => Base.RollbackTransaction();

    public void SaveChanges() => Base.SaveChanges();

    public Task SaveChangesAsync() => Base.SaveChangesAsync();
}
