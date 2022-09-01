using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class InvitationDto
{
    public Guid Guid { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExprationDate { get; set; }
    public string InvitationCode { get; set; }
    public bool IsUsed { get; set; } = false;
    public Guid CreatorGuid { get; set; }
    public DateTime? UsedDate { get; set; }
    public Guid InvitedPersonGuid { get; set; }
    public SchoolRoleEnum SchoolRole { get; set; }
}
