using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;

namespace Gradebook.Foundation.Domain.Models;

public class SystemInvitation : BaseDomainModel
{
    public DateTime CreatedDate { get; set; } = Time.UtcNow;
    public DateTime ExprationDate { get; set; } = Time.UtcNow.AddDays(7);
    public string InvitationCode { get; set; } = string.Empty.GetRandom(6);
    public bool IsUsed { get; set; } = false;

    //**********

    public Guid CreatorGuid { get; set; }
    public DateTime? UsedDate { get; set; }
    public Guid? InvitedPersonGuid { get; set; }
    public Guid SchoolGuid { get; set; }

    //**********

    [ForeignKey("CreatorGuid")]
    public virtual Person? Creator { get; set; }
    [ForeignKey("InvitedPersonGuid")]
    public virtual Person? InvitedPerson { get; set; }
    [ForeignKey("SchoolGuid")]
    public virtual School? School { get; set; }
}
