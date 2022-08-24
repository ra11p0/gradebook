using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Queries;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionQueries : BaseLogic<IPermissionsQueriesRepository>, IPermissionsQueries
{
    public PermissionQueries(IPermissionsQueriesRepository repository) : base(repository)
    {
    }
}
