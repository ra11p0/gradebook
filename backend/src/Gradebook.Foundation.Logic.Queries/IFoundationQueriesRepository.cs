using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public interface IFoundationQueriesRepository : IBaseRepository
{
    Task<Guid?> GetPersonGuidForUser(string userId);
    Task<IEnumerable<SchoolDto>> GetSchoolsForPerson(Guid personGuid);
    Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid);
    Task<IEnumerable<StudentDto>> GetAllAccessibleStudents(Guid relatedPersonGuid);
    Task<IEnumerable<StudentDto>> GetAllInactiveAccessibleStudents(Guid relatedPersonGuid);
    Task<IEnumerable<TeacherDto>> GetAllAccessibleTeachers(Guid relatedPersonGuid);
    Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid);
    Task<InvitationDto> GetInvitationByActivationCode(string activationCode);
    Task<PersonDto> GetPersonByGuid(Guid guid);
    Task<StudentDto> GetStudentByGuid(Guid guid);
    Task<TeacherDto> GetTeacherByGuid(Guid guid);
    Task<GroupDto> GetGroupByGuid(Guid guid);
    Task<ClassDto> GetClassByGuid(Guid guid);
    Task<SchoolDto> GetSchoolByGuid(Guid guid);
    Task<IPagedList<StudentDto>> GetStudentsInSchool(Guid schoolGuid, Pager pager);
}
