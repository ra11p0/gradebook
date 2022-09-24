using Gradebook.Foundation.Common;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommandsRepositoryCached : BaseRepositoryCached<SettingsCommandsRepository, object>, ISettingsCommandsRepository
{
    public SettingsCommandsRepositoryCached(SettingsCommandsRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public void BeginTransaction() => Base.BeginTransaction();

    public void CommitTransaction() => Base.CommitTransaction();

    public void RollbackTransaction() => Base.RollbackTransaction();

    public void SaveChanges() => Base.SaveChanges();

    public Task SaveChangesAsync() => Base.SaveChangesAsync();
}
