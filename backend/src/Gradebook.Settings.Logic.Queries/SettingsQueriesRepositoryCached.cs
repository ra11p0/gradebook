using Gradebook.Foundation.Common;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueriesRepositoryCached : BaseRepositoryCached<SettingsQueriesRepository, object>, ISettingsQueriesRepository
{
    public SettingsQueriesRepositoryCached(SettingsQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public void BeginTransaction() => Base.BeginTransaction();

    public void CommitTransaction() => Base.CommitTransaction();

    public void RollbackTransaction() => Base.RollbackTransaction();

    public void SaveChanges() => Base.SaveChanges();

    public Task SaveChangesAsync() => Base.SaveChangesAsync();
}
