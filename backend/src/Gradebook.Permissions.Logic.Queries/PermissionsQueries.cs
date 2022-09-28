using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Foundation.Common.Permissions.Queries;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueries : BaseLogic<IPermissionsQueriesRepository>, IPermissionsQueries
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public PermissionsQueries(IPermissionsQueriesRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }

    public async Task<IEnumerable<Tuple<PermissionEnum, PermissionLevelEnum>>> GetPermissionsForPerson(Guid personGuid)
    {
        var personResponse = await _foundationQueries.Service.GetPersonByGuid(personGuid);
        if (!personResponse.Status) throw new Exception("Could not get person!");
        var permissions = await Repository.GetPermissionsForPerson(personGuid);
        List<Tuple<PermissionEnum, PermissionLevelEnum>> permissionsWithDefaults = new();
        foreach (var permission in DefaultPermissionLevels.GetDefaultPermissionLevels(personResponse.Response!.SchoolRole))
            permissionsWithDefaults.Add(permissions.FirstOrDefault(p => p.Item1 == permission.Item1) ?? permission);
        return permissionsWithDefaults;
    }
}
