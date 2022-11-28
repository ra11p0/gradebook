namespace Gradebook.Foundation.Common.Foundation;

public interface IFoundationPermissionsLogic
{
    Task<bool> CanInviteToSchool(Guid personGuid);
    Task<bool> CanManageClass(Guid classGuid, Guid personGuid);
    Task<bool> CanCreateNewClass(Guid personGuid);
    Task<bool> CanCreateNewStudents(Guid personGuid);
    Task<bool> CanDeleteStudents(Guid personGuid);
    Task<bool> CanManageSubject(Guid subjectGuid, Guid personGuid);
    Task<bool> CanCreateNewSubject(Guid personGuid);
    Task<bool> CanCreateEducationCycle(Guid schoolGuid);
    Task<bool> CanSeeEducationCycles(Guid schoolGuid);
}
