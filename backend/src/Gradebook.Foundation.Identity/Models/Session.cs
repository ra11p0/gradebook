using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gradebook.Foundation.Identity.Models;

public class Session
{
    [Key]
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
    [ForeignKey("UserId")]
    public virtual ApplicationUser? User { get; set; }
}
