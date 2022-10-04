namespace Gradebook.Foundation.Common.Foundation;

public interface IFoundationPermissionsLogic
{
    Task<bool> CanInviteToSchool(Guid personGuid);
    Task<bool> CanManageClass(Guid classGuid, Guid personGuid);
    Task<bool> CanCreateNewClass(Guid personGuid);
}
