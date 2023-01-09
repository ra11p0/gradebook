namespace Api.Models.Invitations;

public class NewMultipleInvitationModel
{
    public Guid[] InvitedPersonGuidArray { get; set; } = Array.Empty<Guid>();
}
