using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Commands;

public interface IFoundationCommands : IFoundationClassesCommands
{
    Task<ResponseWithStatus<Guid>> AddNewEducationCycle(EducationCycleCommand command);
    Task<StatusResponse> EditTeachersInSubject(Guid subjectGuid, List<Guid> teachersGuids);
    Task<ResponseWithStatus<Guid>> AddSubject(Guid schoolGuid, NewSubjectCommand command);
    Task<ResponseWithStatus<Guid>> AddNewStudent(NewStudentCommand newStudentDto, Guid schoolGuid);
    Task<StatusResponse<bool>> AddNewTeacher(NewTeacherCommand newTeacherCommand, Guid schoolGuid);
    Task<StatusResponse> DeleteSchool(Guid schoolGuid);
    Task<StatusResponse<bool>> NewAdministrator(NewAdministratorCommand command);
    Task<StatusResponse<bool>> NewAdministratorWithSchool(NewAdministratorCommand administratorCommand, NewSchoolCommand schoolCommand);
    Task<ResponseWithStatus<string, bool>> GenerateSystemInvitation(Guid personGuid, SchoolRoleEnum role, Guid schoolGuid);
    Task<ResponseWithStatus<string[], bool>> GenerateMultipleSystemInvitation(Guid[] peopleGuid, SchoolRoleEnum role, Guid schoolGuid);
    Task<StatusResponse<bool>> AddPersonToSchool(Guid schoolGuid, Guid? personGuid = null);
    Task<StatusResponse<bool>> ActivatePerson(string activationCode);
    Task<ResponseWithStatus<Guid>> AddNewClass(NewClassCommand command);
    Task<StatusResponse> DeleteClass(Guid classGuid);
    Task<StatusResponse> DeletePerson(Guid personGuid);
    Task<StatusResponse> AddStudentsToClass(Guid classGuid, IEnumerable<Guid> studentsGuids);
    Task<StatusResponse> AddTeachersToClass(Guid classGuid, IEnumerable<Guid> teachersGuids);
    Task<StatusResponse> DeleteTeachersFromClass(Guid classGuid, IEnumerable<Guid> teachersGuids);
    Task<StatusResponse> DeleteStudentsFromClass(Guid classGuid, IEnumerable<Guid> studentsGuids);
    Task<ResponseWithStatus<IPagedList<StudentDto>>> EditStudentsInClass(Guid classGuid, IEnumerable<Guid> studentsGuids);
    Task<ResponseWithStatus<IPagedList<TeacherDto>>> EditTeachersInClass(Guid classGuid, IEnumerable<Guid> studentsGuids);
    Task<StatusResponse> SetActiveEducationCycleToClass(Guid classGuid, Guid educationCycleGuid);
    Task<StatusResponse> SetActiveEducationCycleToClasses(IEnumerable<Guid> classesGuid, Guid educationCycleGuid);
    Task<StatusResponse> DeleteActiveEducationCycleFromClass(Guid classGuid);
    Task<StatusResponse> EditClassesAssignedToEducationCycle(IEnumerable<Guid> classesGuids, Guid educationCycleGuid);
    Task<StatusResponse> ConfigureEducationCycleForClass(Guid classGuid, EducationCycleConfigurationCommand configuration);
}
