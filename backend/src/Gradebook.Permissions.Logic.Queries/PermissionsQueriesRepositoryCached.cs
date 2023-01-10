using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueriesRepositoryCached : BaseRepositoryCached<PermissionsQueriesRepository, object>, IPermissionsQueriesRepository
{
    public PermissionsQueriesRepositoryCached(PermissionsQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public IDbContextTransaction BeginTransaction()
        => Base.BeginTransaction();


    public void CommitTransaction()
    {
        Base.CommitTransaction();
    }

    public Task<PermissionLevelEnum> GetPermissionForPerson(Guid personGuid, PermissionEnum permissionId)
        => Base.GetPermissionForPerson(personGuid, permissionId);

    public Task<Dictionary<PermissionEnum, PermissionLevelEnum>> GetPermissionsForPerson(Guid personGuid)
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

    IDbContextTransaction IBaseRepository.BeginTransaction()
    {
        throw new NotImplementedException();
    }
}
