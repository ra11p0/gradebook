using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommandsRepositoryCached : BaseRepositoryCached<SettingsCommandsRepository, object>, ISettingsCommandsRepository
{
    public SettingsCommandsRepositoryCached(SettingsCommandsRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public IDbContextTransaction BeginTransaction() => Base.BeginTransaction();

    public void CommitTransaction() => Base.CommitTransaction();

    public void RollbackTransaction() => Base.RollbackTransaction();

    public void SaveChanges() => Base.SaveChanges();

    public Task SaveChangesAsync() => Base.SaveChangesAsync();

    public void SetSettingForPerson<T>(Guid personGuid, SettingEnum settingType, T value)
        => Base.SetSettingForPerson(personGuid, settingType, value);

    public Task SetSettingForPersonAsync<T>(Guid personGuid, SettingEnum settingType, T value)
        => Base.SetSettingForPersonAsync(personGuid, settingType, value);

    public void SetSettingForUser<T>(string userGuid, SettingEnum settingType, T value)
        => Base.SetSettingForUser(userGuid, settingType, value);

    public Task SetSettingForUserAsync<T>(string userGuid, SettingEnum settingType, T value)
        => Base.SetSettingForUserAsync(userGuid, settingType, value);

    IDbContextTransaction IBaseRepository.BeginTransaction()
    {
        throw new NotImplementedException();
    }
}
