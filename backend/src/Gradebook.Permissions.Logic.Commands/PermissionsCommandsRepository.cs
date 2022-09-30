using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Permissions.Database;
using Gradebook.Permissions.Domain.Models;

namespace Gradebook.Permissions.Logic.Commands;

public class PermissionsCommandsRepository : BaseRepository<PermissionsDatabaseContext>, IPermissionsCommandsRepository
{
    public PermissionsCommandsRepository(PermissionsDatabaseContext context) : base(context)
    {
    }

    public async Task SetPermissionsForPerson(Guid personGuid, Dictionary<PermissionEnum, PermissionLevelEnum> permissions)
    {
        var currentPermissions = Context.Permissions!.Where(e => e.PersonGuid == personGuid);
        foreach (var permission in permissions)
        {
            if (currentPermissions.Any(e => e.PermissionId == permission.Key))
                currentPermissions.First(e => e.PermissionId == permission.Key).PermissionLevel = permission.Value;
            else await Context.Permissions!.AddAsync(new Permission()
            {
                PersonGuid = personGuid,
                PermissionId = permission.Key,
                PermissionLevel = permission.Value
            });
        }
    }
}
