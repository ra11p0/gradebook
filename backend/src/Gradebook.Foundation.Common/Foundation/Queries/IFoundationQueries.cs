using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationQueries
{
    Task<InvitationDto> GetStudentInvitationByGuid(Guid guid);
    Task<ResponseWithStatus<IEnumerable<SchoolDto>, bool>> GetSchoolsForPerson(Guid personGuid);
    Task<ResponseWithStatus<Guid, bool>> GetPersonGuidForUser(string userId);
    Task<ResponseWithStatus<IEnumerable<PersonDto>, bool>> GetPeopleInSchool(Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> GetCurrentPersonGuid();
}
