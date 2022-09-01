namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class UseInvitationCommand
{
    public Guid InvitationGuid { get; set; }
    public string UserGuid { get; set; }
    public DateTime UsedDate { get; set; }

}
