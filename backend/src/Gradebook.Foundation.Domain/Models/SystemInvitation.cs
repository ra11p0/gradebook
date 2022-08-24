using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class SystemInvitation : BaseDomainModel
{
    public DateTime CreatedDate { get; set; }
    public DateTime ExprationDate { get; set; }
    public Guid CreatorGuid { get; set; }
    [ForeignKey("CreatorGuid")]
    public Person Creator { get; set; }
    public string InvitationCode { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedDate { get; set; }
    public Guid InvitedPersonGuid { get; set; }
    [ForeignKey("InvitedPersonGuid")]
    public Person InvitedPerson { get; set; }
    public string Role { get; set; }
}
