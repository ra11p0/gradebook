using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gradebook.Permissions.Logic.Commands;

public class PermissionsCommandsRepositoryCached : BaseRepositoryCached<PermissionsCommandsRepository, object>, IPermissionsCommandsRepository
{
    public PermissionsCommandsRepositoryCached(PermissionsCommandsRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public IDbContextTransaction BeginTransaction()
        => Base.BeginTransaction();


    public void CommitTransaction()
    {
        Base.CommitTransaction();
    }

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

    public Task SetPermissionsForPerson(Guid personGuid, Dictionary<PermissionEnum, PermissionLevelEnum> permissions)
        => Base.SetPermissionsForPerson(personGuid, permissions);

    IDbContextTransaction IBaseRepository.BeginTransaction()
    {
        throw new NotImplementedException();
    }
}
