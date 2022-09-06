using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationQueries
{
    Task<ResponseWithStatus<IEnumerable<SchoolDto>, bool>> GetSchoolsForPerson(Guid personGuid);
    Task<ResponseWithStatus<Guid, bool>> GetPersonGuidForUser(string userId);
    Task<ResponseWithStatus<IEnumerable<PersonDto>, bool>> GetPeopleInSchool(Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> GetCurrentPersonGuid();
    Task<ResponseWithStatus<IEnumerable<StudentDto>, bool>> GetAllAccessibleStudents();
    Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations(Guid personGuid);
    Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations();
    Task<ResponseWithStatus<IEnumerable<TeacherDto>, bool>> GetAllAccessibleTeachers();
    Task<ResponseWithStatus<InvitationDto, bool>> GetInvitationByActivationCode(string activationCode);
    Task<ResponseWithStatus<PersonDto, bool>> GetPersonByGuid(Guid guid);
    Task<ResponseWithStatus<ActivationCodeInfoDto, bool>> GetActivationCodeInfo(string activationCode, string method);
    Task<ResponseWithStatus<ClassDto, bool>> GetClassByGuid(Guid guid);
    Task<ResponseWithStatus<GroupDto, bool>> GetGroupByGuid(Guid guid);
    Task<ResponseWithStatus<StudentDto, bool>> GetStudentByGuid(Guid guid);
    Task<ResponseWithStatus<TeacherDto, bool>> GetTeacherByGuid(Guid guid);
}
