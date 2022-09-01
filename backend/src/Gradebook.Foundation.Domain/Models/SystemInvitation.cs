using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Domain.Models;

public class SystemInvitation : BaseDomainModel
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime ExprationDate { get; set; } = DateTime.Now.AddDays(7);
    public string InvitationCode { get; set; } = new string("").GetRandom(6);
    public bool IsUsed { get; set; } = false;

    //**********

    public Guid CreatorGuid { get; set; }
    public DateTime? UsedDate { get; set; }
    public Guid? InvitedPersonGuid { get; set; }
    public SchoolRoleEnum SchoolRole { get; set; }

    //**********

    [ForeignKey("CreatorGuid")]
    public virtual Person Creator { get; set; }
    [ForeignKey("InvitedPersonGuid")]
    public virtual Person? InvitedPerson { get; set; }
}
