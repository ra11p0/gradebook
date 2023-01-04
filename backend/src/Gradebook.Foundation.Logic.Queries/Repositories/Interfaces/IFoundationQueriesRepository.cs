using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;

public interface IFoundationQueriesRepository : IBaseRepository,
    IFoundationQueriesClassesRepository,
    IFoundationQueriesPeopleRepository,
    IFoundationQueriesInvitationsRepository
{
    Task<EducationCycleExtendedDto?> GetEducationCycle(Guid educationCycleGuid);
    Task<IEnumerable<EducationCycleStepDto>> GetStepsForEducationCycle(Guid educationCycleGuid);
    Task<IEnumerable<EducationCycleStepSubjectDto>> GetStepsSubjectsForEducationCycleStep(Guid educationCycleStepGuid);
    Task<IPagedList<EducationCycleDto>> GetEducationCyclesInSchool(Guid schoolGuid, Pager pager, string query);
    Task<IPagedList<SubjectDto>> GetSubjectsForTeacher(Guid teacherGuid, Pager pager);
    Task<IPagedList<TeacherDto>> GetTeachersForSubject(Guid subjectGuid, Pager pager, string? query);
    Task<SubjectDto> GetSubject(Guid subjectGuid);
    Task<IPagedList<SubjectDto>> GetSubjectsForSchool(Guid schoolGuid, Pager pager, string query);
    Task<Guid?> GetPersonGuidForUser(string userId, Guid schoolGuid);
    Task<IEnumerable<SchoolDto>> GetSchoolsForUser(string userGuid);
    Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid);
    Task<GroupDto> GetGroupByGuid(Guid guid);
    Task<ClassDto?> GetClassByGuid(Guid guid);
    Task<IEnumerable<ClassDto>> GetClassesByGuids(IEnumerable<Guid> guids);
    Task<SchoolDto> GetSchoolByGuid(Guid guid);
    Task<IPagedList<StudentDto>> GetStudentsInSchool(Guid schoolGuid, Pager pager);
    Task<IPagedList<ClassDto>> GetClassesInSchool(Guid schoolGuid, Pager pager, string? query = "");
    Task<IPagedList<ClassDto>> GetClassesForPerson(Guid personGuid, Pager pager);
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
    Task<IPagedList<Guid>> GetClassesGuidsForEducationCycle(Guid educationCycle, string? query, Pager pager);
    Task<IEnumerable<EducationCycleInstanceDto>> GetEducationCycleInstancesByGuids(IEnumerable<Guid> guids);
    Task<IEnumerable<EducationCycleStepInstanceDto>> GetEducationCycleStepInstancesByEducationCycleInstancesGuids(IEnumerable<Guid> guids);
    Task<IEnumerable<EducationCycleStepSubjectInstanceDto>> GetEducationCycleStepSubjectInstancesByEducationCycleStepInstancesGuids(IEnumerable<Guid> guids);
    Task<Guid?> GetActiveEducationCycleGuidByClassGuid(Guid classGuid);
    Task<IPagedList<Guid>> GetEducationCycleInstancesGuidsByClassGuid(Guid classGuid, Pager pager);
    Task<Guid?> GetEducationCycleInstanceForClass(Guid classGuid, Guid educationCycleGuid);
    Task<IPagedList<EducationCycleExtendedDto>> GetEducationCyclesByGuids(IEnumerable<Guid> guids, Pager pager);
    Task<IPagedList<ClassDto>> GetAvailableClassesWithAssignedForEducationCycle(Guid educationCycleGuid, Pager pager, string? query);
}
