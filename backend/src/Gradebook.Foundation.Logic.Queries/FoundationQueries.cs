using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationQueries : BaseLogic<IFoundationQueriesRepository>, IFoundationQueries
{
    public FoundationQueries(IFoundationQueriesRepository repository) : base(repository)
    {
    }

    public Task<InvitationDto> GetStudentInvitationByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }
}
