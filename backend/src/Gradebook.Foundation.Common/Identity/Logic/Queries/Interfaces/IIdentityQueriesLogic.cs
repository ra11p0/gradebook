using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gradebook.Foundation.Common.Identity.Logic.Queries.Interfaces;

public interface IIdentityQueriesLogic
{
    JwtSecurityToken CreateToken(List<Claim> authClaims);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}
