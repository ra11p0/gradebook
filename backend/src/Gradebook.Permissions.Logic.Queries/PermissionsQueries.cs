using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Queries;

namespace Gradebook.Permissions.Logic.Queries;

public class PermissionsQueries : BaseLogic<IPermissionsQueriesRepository>, IPermissionsQueries
{
    public PermissionsQueries(IPermissionsQueriesRepository repository) : base(repository)
    {
    }
}
