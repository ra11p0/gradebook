using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public class FoundationQueriesRepositoryCached : BaseRepositoryCached<FoundationQueriesRepository, object>, IFoundationQueriesRepository
{
    public FoundationQueriesRepositoryCached(FoundationQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public Task<IEnumerable<StudentDto>> GetAllAccessibleStudents(Guid relatedPersonGuid)
        => Base.GetAllAccessibleStudents(relatedPersonGuid);

    public Task<IEnumerable<TeacherDto>> GetAllAccessibleTeachers(Guid relatedPersonGuid)
        => Base.GetAllAccessibleTeachers(relatedPersonGuid);

    public Task<IEnumerable<StudentDto>> GetAllInactiveAccessibleStudents(Guid schoolGuid)
        => Base.GetAllInactiveAccessibleStudents(schoolGuid);

    public Task<ClassDto> GetClassByGuid(Guid guid)
        => Base.GetClassByGuid(guid);

    public Task<GroupDto> GetGroupByGuid(Guid guid)
        => Base.GetGroupByGuid(guid);

    public Task<InvitationDto> GetInvitationByActivationCode(string activationCode)
        => Base.GetInvitationByActivationCode(activationCode);

    public Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid)
        => Base.GetInvitations(personGuid);

    public Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid)
        => Base.GetPeopleInSchool(schoolGuid);

    public Task<PersonDto> GetPersonByGuid(Guid guid)
        => Base.GetPersonByGuid(guid);

    public Task<Guid?> GetPersonGuidForUser(string userId, Guid schoolGuid)
        => Base.GetPersonGuidForUser(userId, schoolGuid);

    public Task<IEnumerable<SchoolDto>> GetSchoolsForUser(string userGuid)
        => Base.GetSchoolsForUser(userGuid);

    public Task<StudentDto> GetStudentByGuid(Guid guid)
        => Base.GetStudentByGuid(guid);

    public Task<TeacherDto> GetTeacherByGuid(Guid guid)
        => Base.GetTeacherByGuid(guid);

    public void BeginTransaction()
        => Base.BeginTransaction();

    public void CommitTransaction()
        => Base.CommitTransaction();

    public void RollbackTransaction()
        => Base.RollbackTransaction();

    public void SaveChanges()
        => Base.SaveChanges();

    public Task SaveChangesAsync()
        => Base.SaveChangesAsync();

    public Task<SchoolDto> GetSchoolByGuid(Guid guid)
        => Base.GetSchoolByGuid(guid);

    public Task<IPagedList<StudentDto>> GetStudentsInSchool(Guid schoolGuid, Pager pager)
        => Base.GetStudentsInSchool(schoolGuid, pager);

    public Task<IPagedList<InvitationDto>> GetInvitationsToSchool(Guid schoolGuid, Pager pager)
        => Base.GetInvitationsToSchool(schoolGuid, pager);

    public Task<IPagedList<ClassDto>> GetClassesInSchool(Guid schoolGuid, Pager pager)
        => Base.GetClassesInSchool(schoolGuid, pager);

    public Task<IPagedList<StudentDto>> GetStudentsInClass(Guid schoolGuid, Pager pager)
        => Base.GetStudentsInClass(schoolGuid, pager);

    public Task<IPagedList<TeacherDto>> GetTeachersInClass(Guid schoolGuid, Pager pager)
        => Base.GetTeachersInClass(schoolGuid, pager);

    public Task<IPagedList<TeacherDto>> GetTeachersInSchool(Guid schoolGuid, Pager pager)
        => Base.GetTeachersInSchool(schoolGuid, pager);

    public Task<IPagedList<PersonDto>> GetPeopleInSchool(Guid schoolGuid, string schoolRole, string query, Pager pager)
        => Base.GetPeopleInSchool(schoolGuid, schoolRole, query, pager);

    public Task<bool> IsUserActive(string userGuid)
        => Base.IsUserActive(userGuid);

    public Task<IEnumerable<StudentDto>> GetAllStudentsInClass(Guid classGuid)
        => Base.GetAllStudentsInClass(classGuid);

    public Task<IEnumerable<TeacherDto>> GetAllTeachersInClass(Guid classGuid)
        => Base.GetAllTeachersInClass(classGuid);

    public Task<bool> IsClassOwner(Guid classGuid, Guid personGuid)
        => Base.IsClassOwner(classGuid, personGuid);

    public Task<IPagedList<StudentDto>> SearchStudentsCandidatesToClassWithCurrent(Guid classGuid, string query, Pager pager)
        => Base.SearchStudentsCandidatesToClassWithCurrent(classGuid, query, pager);

    public Task<bool> IsStudentInAnyClass(Guid studentGuid)
        => Base.IsStudentInAnyClass(studentGuid);

    public Task<IPagedList<ClassDto>> GetClassesForPerson(Guid personGuid, Pager pager)
        => Base.GetClassesForPerson(personGuid, pager);

    public Task<SubjectDto> GetSubject(Guid subjectGuid)
        => Base.GetSubject(subjectGuid);

    public Task<IPagedList<SubjectDto>> GetSubjectsForSchool(Guid schoolGuid, Pager pager, string query)
        => Base.GetSubjectsForSchool(schoolGuid, pager, query);

    public Task<IPagedList<TeacherDto>> GetTeachersForSubject(Guid subjectGuid, Pager pager)
        => Base.GetTeachersForSubject(subjectGuid, pager);

    public Task<IPagedList<SubjectDto>> GetSubjectsForTeacher(Guid teacherGuid, Pager pager)
        => Base.GetSubjectsForTeacher(teacherGuid, pager);

    public Task<IPagedList<EducationCycleDto>> GetEducationCyclesInSchool(Guid schoolGuid, Pager pager)
        => Base.GetEducationCyclesInSchool(schoolGuid, pager);

    public Task<EducationCycleExtendedDto?> GetEducationCycle(Guid educationCycleGuid)
        => Base.GetEducationCycle(educationCycleGuid);

    public Task<IEnumerable<EducationCycleStepDto>> GetStepsForEducationCycle(Guid educationCycleGuid)
        => Base.GetStepsForEducationCycle(educationCycleGuid);

    public Task<IEnumerable<EducationCycleStepSubjectDto>> GetStepsSubjectsForEducationCycleStep(Guid educationCycleStepGuid)
        => Base.GetStepsSubjectsForEducationCycleStep(educationCycleStepGuid);

    public Task<IPagedList<Guid>> GetClassesGuidsForEducationCycle(Guid educationCycle, Pager pager)
        => Base.GetClassesGuidsForEducationCycle(educationCycle, pager);
}
