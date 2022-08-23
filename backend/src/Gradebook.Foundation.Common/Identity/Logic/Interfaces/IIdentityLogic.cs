using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gradebook.Foundation.Common.Identity.Logic.Interfaces;

public interface IIdentityLogic
{
    JwtSecurityToken CreateToken(List<Claim> authClaims);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    Task EditUserRoles(string userGuid, string[] roles);
    Task<ResponseWithStatus<string[], bool>> GetUserRoles(string userGuid);
    Task<ResponseWithStatus<string, bool>> CurrentUserId();
}
