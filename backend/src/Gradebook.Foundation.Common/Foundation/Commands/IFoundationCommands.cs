using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Commands;

public interface IFoundationCommands
{
    Task<StatusResponse<bool>> AddNewStudent(NewStudentCommand newStudentDto);
    Task<StatusResponse<bool>> AddNewTeacher(NewTeacherCommand newTeacherCommand);
    Task<StatusResponse> AddNewSchool(NewSchoolCommand newSchoolCommand);
    Task<StatusResponse> DeleteSchool(Guid schoolGuid);
    Task<StatusResponse<bool>> NewAdministrator(NewAdministratorCommand command);
    Task<StatusResponse<bool>> NewAdministratorWithSchool(NewAdministratorCommand administratorCommand, NewSchoolCommand schoolCommand);
    Task<ResponseWithStatus<string, bool>> GenerateSystemInvitation(Guid personGuid, SchoolRoleEnum role);
    Task<ResponseWithStatus<string[], bool>> GenerateMultipleSystemInvitation(Guid[] peopleGuid, SchoolRoleEnum role);
    Task<StatusResponse<bool>> AddPersonToSchool(Guid schoolGuid, Guid? personGuid = null);
    Task<StatusResponse<bool>> ActivatePerson(string activationCode);
}
