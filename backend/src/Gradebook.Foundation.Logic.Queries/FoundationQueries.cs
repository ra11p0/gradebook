using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationQueries : BaseLogic<IFoundationQueriesRepository>, IFoundationQueries
{
    public FoundationQueries(IFoundationQueriesRepository repository) : base(repository)
    {
    }
}
