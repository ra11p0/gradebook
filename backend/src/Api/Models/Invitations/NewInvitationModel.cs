using Gradebook.Foundation.Common.Foundation.Enums;

namespace Api.Models.Invitations;

public class NewInvitationModel
{
    public Guid InvitedPersonGuid { get; set; }
    public SchoolRoleEnum Role { get; set; }
}
