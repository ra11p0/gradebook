using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationQueries
{
    Task<InvitationDto> GetStudentInvitationByGuid(Guid guid);
    Task<ResponseWithStatus<IEnumerable<SchoolDto>, bool>> GetSchoolsForPerson(Guid personGuid);
    Task<ResponseWithStatus<Guid, bool>> GetPersonGuidForUser(string userId);
    Task<ResponseWithStatus<IEnumerable<PersonDto>, bool>> GetPeopleInSchool(Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> GetCurrentPersonGuid();
    Task<ResponseWithStatus<IEnumerable<StudentDto>, bool>> GetAllAccessibleStudents();
    Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations(Guid personGuid);
    Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations();
    Task<ResponseWithStatus<IEnumerable<TeacherDto>, bool>> GetAllAccessibleTeachers();
}
