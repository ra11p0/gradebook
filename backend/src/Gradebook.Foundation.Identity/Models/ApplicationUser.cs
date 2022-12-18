using Microsoft.AspNetCore.Identity;

namespace Gradebook.Foundation.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Session>? Sessions { get; set; }
    public virtual ICollection<AuthorizationCode>? AuthorizationCodes { get; set; }
}
