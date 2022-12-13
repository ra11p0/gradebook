using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Logic.Commands.Repositories;

public interface IFoundationCommandsRepository : IBaseRepository, IFoundationCommandsClassesRepository
{
    Task<ResponseWithStatus<Guid>> AddNewEducationCycle(EducationCycleCommand command);
    Task<StatusResponse> AddTeachersToSubject(Guid subjectGuid, List<Guid> teachersGuids);
    Task<StatusResponse> RemoveTeachersFromSubject(Guid subjectGuid, List<Guid> teachersGuids);
    Task<ResponseWithStatus<Guid>> AddNewStudent(NewStudentCommand newStudentDto);
    Task<ResponseWithStatus<Guid, bool>> AddNewTeacher(NewTeacherCommand newTeacherDto);
    Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command);
    Task<StatusResponse<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command);
    Task<string?> GenerateSystemInvitation(Guid invitedPersonGuid, Guid invitingPersonGuid, SchoolRoleEnum role, Guid schoolGuid);
    Task<StatusResponse<bool>> AddPersonToSchool(Guid schoolGuid, Guid personGuid);
    Task<StatusResponse<bool>> AssignUserToStudent(string userId, Guid personGuid);
    Task<StatusResponse<bool>> AssignUserToAdministrator(string userId, Guid personGuid);
    Task<StatusResponse<bool>> AssignUserToTeacher(string userId, Guid personGuid);
    Task<StatusResponse<bool>> UseInvitation(UseInvitationCommand command);
    Task<StatusResponse> DeleteSchool(Guid schoolGuid);
    Task<ResponseWithStatus<Guid>> AddNewClass(NewClassCommand command);
    Task<StatusResponse> DeleteClass(Guid classGuid);
    Task<StatusResponse> DeletePerson(Guid personGuid);
    Task<ResponseWithStatus<Guid>> AddSubject(Guid schoolGuid, NewSubjectCommand command);
}
