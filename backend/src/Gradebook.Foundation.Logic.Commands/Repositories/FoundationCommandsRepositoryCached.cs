using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Logic.Commands.Repositories;

public class FoundationCommandsRepositoryCached : BaseRepositoryCached<FoundationCommandsRepository, object>, IFoundationCommandsRepository
{
    public FoundationCommandsRepositoryCached(FoundationCommandsRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public Task<StatusResponse<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid)
        => Base.AddAdministratorToSchool(administratorGuid, schoolGuid);

    public Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command)
        => Base.AddNewAdministrator(command);

    public Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command)
        => Base.AddNewSchool(command);

    public Task<ResponseWithStatus<Guid, bool>> AddNewTeacher(NewTeacherCommand newTeacherDto)
        => Base.AddNewTeacher(newTeacherDto);

    public Task<StatusResponse<bool>> AddPersonToSchool(Guid schoolGuid, Guid personGuid)
        => Base.AddPersonToSchool(schoolGuid, personGuid);

    public Task<StatusResponse<bool>> AssignUserToAdministrator(string userId, Guid personGuid)
        => Base.AssignUserToAdministrator(userId, personGuid);

    public Task<StatusResponse<bool>> AssignUserToStudent(string userId, Guid personGuid)
        => Base.AssignUserToStudent(userId, personGuid);

    public Task<StatusResponse<bool>> AssignUserToTeacher(string userId, Guid personGuid)
        => Base.AssignUserToTeacher(userId, personGuid);

    public Task<string?> GenerateSystemInvitation(Guid invitedPersonGuid, Guid invitingPersonGuid, SchoolRoleEnum role, Guid schoolGuid)
        => Base.GenerateSystemInvitation(invitedPersonGuid, invitingPersonGuid, role, schoolGuid);
    public void BeginTransaction()
    {
        Base.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Base.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        Base.RollbackTransaction();
    }

    public void SaveChanges()
    {
        Base.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Base.SaveChangesAsync();
    }

    public Task<StatusResponse<bool>> UseInvitation(UseInvitationCommand command)
        => Base.UseInvitation(command);

    public Task<StatusResponse> DeleteSchool(Guid schoolGuid)
        => Base.DeleteSchool(schoolGuid);

    public Task<ResponseWithStatus<Guid>> AddNewStudent(NewStudentCommand newStudentDto)
        => Base.AddNewStudent(newStudentDto);

    public Task<ResponseWithStatus<Guid>> AddNewClass(NewClassCommand command)
        => Base.AddNewClass(command);

    public Task<StatusResponse> DeleteClass(Guid classGuid)
        => Base.DeleteClass(classGuid);

    public Task<StatusResponse> DeletePerson(Guid personGuid)
        => Base.DeletePerson(personGuid);

    public Task<StatusResponse> AddStudentsToClass(Guid classGuid, IEnumerable<Guid> studentsGuids)
        => Base.AddStudentsToClass(classGuid, studentsGuids);

    public Task<StatusResponse> AddTeachersToClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
        => Base.AddTeachersToClass(classGuid, teachersGuids);

    public Task<StatusResponse> DeleteStudentsFromClass(Guid classGuid, IEnumerable<Guid> studentsGuids)
        => Base.DeleteStudentsFromClass(classGuid, studentsGuids);

    public Task<StatusResponse> DeleteTeachersFromClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
        => Base.DeleteTeachersFromClass(classGuid, teachersGuids);

    public Task<StatusResponse> SetStudentActiveClass(Guid classGuid, Guid studentGuid)
        => Base.SetStudentActiveClass(classGuid, studentGuid);

    public Task<StatusResponse> RemoveStudentActiveClass(Guid studentGuid)
        => Base.RemoveStudentActiveClass(studentGuid);

    public Task<ResponseWithStatus<Guid>> AddSubject(Guid schoolGuid, NewSubjectCommand command)
        => Base.AddSubject(schoolGuid, command);

    public Task<StatusResponse> AddTeachersToSubject(Guid subjectGuid, List<Guid> teachersGuids)
        => Base.AddTeachersToSubject(subjectGuid, teachersGuids);

    public Task<StatusResponse> RemoveTeachersFromSubject(Guid subjectGuid, List<Guid> teachersGuids)
        => Base.RemoveTeachersFromSubject(subjectGuid, teachersGuids);

    public Task<ResponseWithStatus<Guid>> AddNewEducationCycle(EducationCycleCommand command)
        => Base.AddNewEducationCycle(command);

    public Task<StatusResponse> SetActiveEducationCycleToClasses(IEnumerable<Guid> classesGuid, Guid educationCycleGuid)
        => Base.SetActiveEducationCycleToClasses(classesGuid, educationCycleGuid);

    public Task<StatusResponse> DeleteActiveEducationCycleFromClasses(IEnumerable<Guid> classesGuids)
        => Base.DeleteActiveEducationCycleFromClasses(classesGuids);
    public Task<StatusResponse> ConfigureEducationCycleForClass(Guid classGuid, Guid creatorGuid, EducationCycleConfigurationCommand configuration)
        => Base.ConfigureEducationCycleForClass(classGuid, creatorGuid, configuration);

    public Task<StatusResponse> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, IEnumerable<EducationCycleConfigurationStageCommand> stageCommands)
        => Base.ConfigureEducationCycleStageInstanceForEducationCycleInstance(educationCycleInstanceGuid, stageCommands);

    public Task<StatusResponse> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, EducationCycleConfigurationStageCommand stageCommand)
        => Base.ConfigureEducationCycleStageInstanceForEducationCycleInstance(educationCycleInstanceGuid, stageCommand);

    public Task<StatusResponse> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, IEnumerable<EducationCycleConfigurationSubjectCommand> subjectCommands)
        => Base.ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(educationCycleStepInstanceGuid, subjectCommands);

    public Task<StatusResponse> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, EducationCycleConfigurationSubjectCommand subjectCommand)
        => Base.ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(educationCycleStepInstanceGuid, subjectCommand);
}
