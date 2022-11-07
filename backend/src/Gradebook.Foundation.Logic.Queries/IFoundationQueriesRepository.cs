using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public interface IFoundationQueriesRepository : IBaseRepository
{
    Task<Guid?> GetPersonGuidForUser(string userId, Guid schoolGuid);
    Task<IEnumerable<SchoolDto>> GetSchoolsForUser(string userGuid);
    Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid);
    Task<IEnumerable<StudentDto>> GetAllAccessibleStudents(Guid schoolGuid);
    Task<IEnumerable<StudentDto>> GetAllInactiveAccessibleStudents(Guid schoolGuid);
    Task<IEnumerable<TeacherDto>> GetAllAccessibleTeachers(Guid relatedPersonGuid);
    Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid);
    Task<IPagedList<InvitationDto>> GetInvitationsToSchool(Guid schoolGuid, Pager pager);
    Task<InvitationDto> GetInvitationByActivationCode(string activationCode);
    Task<PersonDto> GetPersonByGuid(Guid guid);
    Task<StudentDto> GetStudentByGuid(Guid guid);
    Task<TeacherDto> GetTeacherByGuid(Guid guid);
    Task<GroupDto> GetGroupByGuid(Guid guid);
    Task<ClassDto> GetClassByGuid(Guid guid);
    Task<SchoolDto> GetSchoolByGuid(Guid guid);
    Task<IPagedList<StudentDto>> GetStudentsInSchool(Guid schoolGuid, Pager pager);
    Task<IPagedList<ClassDto>> GetClassesInSchool(Guid schoolGuid, Pager pager);
    Task<IPagedList<StudentDto>> GetStudentsInClass(Guid classGuid, Pager pager);
    Task<IPagedList<TeacherDto>> GetTeachersInClass(Guid classGuid, Pager pager);
    Task<IPagedList<StudentDto>> SearchStudentsCandidatesToClassWithCurrent(Guid classGuid, string query, Pager pager);
    Task<IEnumerable<StudentDto>> GetAllStudentsInClass(Guid classGuid);
    Task<IEnumerable<TeacherDto>> GetAllTeachersInClass(Guid classGuid);
    Task<IPagedList<TeacherDto>> GetTeachersInSchool(Guid schoolGuid, Pager pager);
    Task<IPagedList<PersonDto>> GetPeopleInSchool(Guid schoolGuid, string discriminator, string query, Pager pager);
    Task<bool> IsClassOwner(Guid classGuid, Guid personGuid);
    Task<bool> IsUserActive(string userGuid);
    Task<bool> IsStudentInAnyClass(Guid studentGuid);
}
