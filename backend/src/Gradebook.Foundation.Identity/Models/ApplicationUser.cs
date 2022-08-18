using Microsoft.AspNetCore.Identity;

namespace Gradebook.Foundation.Identity.Models;

public class ApplicationUser: IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
