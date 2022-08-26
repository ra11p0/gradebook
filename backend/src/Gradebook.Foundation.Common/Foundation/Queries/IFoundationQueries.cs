using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationQueries
{
    Task<InvitationDto> GetStudentInvitationByGuid(Guid guid);
}
