using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueriesRepositoryCached : BaseRepositoryCached<PermissionsQueriesRepository, object>, IPermissionsQueriesRepository
{
    public PermissionsQueriesRepositoryCached(PermissionsQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public void BeginTransaction()
    {
        Base.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Base.CommitTransaction();
    }

    public IEnumerable<Tuple<PermissionEnum, PermissionLevelEnum>> GetPermissionsForPerson(Guid personGuid)
        => Base.GetPermissionsForPerson(personGuid);

    public void RollbackTransaction()
    {
        Base.RollbackTransaction();
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
