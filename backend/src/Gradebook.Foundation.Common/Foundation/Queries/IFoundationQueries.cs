using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationQueries : IFoundationEducationCyclesQueries, IFoundationClassesQueries
{
    Task<ResponseWithStatus<IEnumerable<TeacherDto>, bool>> GetAllAccessibleTeachers(Guid schoolGuid);
    Task<ResponseWithStatus<Guid>> GetCurrentPersonGuidBySubjectGuid(Guid subjectGuid);
    Task<ResponseWithStatus<IPagedList<SubjectDto>>> GetSubjectsForTeacher(Guid teacherGuid, int page);
    Task<ResponseWithStatus<IPagedList<TeacherDto>>> GetTeachersForSubject(Guid subjectGuid, int page, string? query = "");
    Task<ResponseWithStatus<SubjectDto>> GetSubject(Guid subjectGuid);
    Task<ResponseWithStatus<IPagedList<SubjectDto>>> GetSubjectsForSchool(Guid schoolGuid, int page, string query);
    Task<ResponseWithStatus<IEnumerable<SchoolWithRelatedPersonDto>, bool>> GetSchoolsForUser(string userGuid);
    Task<ResponseWithStatus<IEnumerable<PersonDto>>> GetPeopleByUserGuid(string userGuid);
    Task<ResponseWithStatus<bool>> IsUserActive(string userGuid);
    Task<ResponseWithStatus<SchoolDto>> GetSchool(Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> GetPersonGuidForUser(string userId, Guid schoolGuid);
    Task<ResponseWithStatus<IEnumerable<PersonDto>, bool>> GetPeopleInSchool(Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> GetCurrentPersonGuid(Guid schoolGuid);
    Task<ResponseWithStatus<IEnumerable<StudentDto>, bool>> GetAllAccessibleStudents(Guid schoolGuid);
    Task<ResponseWithStatus<IEnumerable<StudentDto>>> GetInactiveStudents(Guid schoolGuid);
    Task<ResponseWithStatus<IEnumerable<TeacherDto>>> GetInactiveTeachers(Guid schoolGuid);
    Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations(Guid personGuid);
    Task<ResponseWithStatus<IPagedList<InvitationDto>, bool>> GetInvitationsToSchool(Guid schoolGuid, int page);
    Task<ResponseWithStatus<InvitationDto, bool>> GetInvitationByActivationCode(string activationCode);
    Task<ResponseWithStatus<PersonDto, bool>> GetPersonByGuid(Guid guid);
    Task<ResponseWithStatus<ActivationCodeInfoDto>> GetActivationCodeInfo(string activationCode, string method);
    Task<ResponseWithStatus<GroupDto, bool>> GetGroupByGuid(Guid guid);
    Task<ResponseWithStatus<StudentDto, bool>> GetStudentByGuid(Guid guid);
    Task<ResponseWithStatus<TeacherDto, bool>> GetTeacherByGuid(Guid guid);
    Task<ResponseWithStatus<IPagedList<StudentDto>>> GetStudentsInSchool(Guid schoolGuid, int page);
    Task<ResponseWithStatus<IPagedList<TeacherDto>>> GetTeachersInSchool(Guid schoolGuid, int page);
    Task<ResponseWithStatus<IPagedList<PersonDto>>> GetPeopleInSchool(Guid schoolGuid, string discriminator, string query, int page);
    Task<ResponseWithStatus<IPagedList<StudentDto>>> SearchStudentsCandidatesToClassWithCurrent(Guid classGuid, string query, int page);
    Task<ResponseWithStatus<Guid>> RecogniseCurrentPersonByRelatedPerson(Guid requestedPerson);
    Task<ResponseWithStatus<Guid>> RecogniseCurrentPersonBySchoolGuid(Guid schoolGuid);
    Task<ResponseWithStatus<Guid>> RecogniseCurrentPersonByClassGuid(Guid classGuid);
    Task<ResponseWithStatus<bool>> IsStudentInAnyClass(Guid studentGuid);
    Task<ResponseWithStatus<IPagedList<Guid>>> GetClassesGuidsForEducationCycle(Guid educationCycle, int page, string? query = null);
    Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesForEducationCycle(Guid educationCycle, int page, string? query = null);
    Task<ResponseWithStatus<EducationCyclesForClassDto>> GetEducationCyclesByClassGuid(Guid classGuid);
}
