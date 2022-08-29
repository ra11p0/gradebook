using Gradebook.Foundation.Common;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueriesRepositoryCached : BaseRepositoryCached<PermissionsQueriesRepository, object>, IPermissionsQueriesRepository
{
    public PermissionsQueriesRepositoryCached(PermissionsQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public void SaveChanges()
    {
        Base.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Base.SaveChangesAsync();
    }
}
