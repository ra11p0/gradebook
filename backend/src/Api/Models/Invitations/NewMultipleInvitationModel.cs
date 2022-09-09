using Gradebook.Foundation.Common.Foundation.Enums;

namespace Api.Models.Invitations;

public class NewMultipleInvitationModel
{
    public Guid[] InvitedPersonGuidArray { get; set; }
    public SchoolRoleEnum Role { get; set; }
}
