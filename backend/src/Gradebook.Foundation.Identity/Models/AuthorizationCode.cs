using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;

namespace Gradebook.Foundation.Identity.Models;

public class AuthorizationCode
{
    [Key]
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty.GetRandom(8);
    public bool IsUsed { get; set; } = false;
    public DateTime AuthorizationCodeValidUntil { get; set; } = Time.UtcNow.AddMinutes(30);
    [ForeignKey("UserId")]
    public virtual ApplicationUser? User { get; set; }
}
