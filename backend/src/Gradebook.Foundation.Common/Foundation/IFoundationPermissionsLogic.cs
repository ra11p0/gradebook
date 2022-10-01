namespace Gradebook.Foundation.Common.Foundation;

public interface IFoundationPermissionsLogic
{
    Task<bool> CanInviteToSchool(Guid personGuid);
}
