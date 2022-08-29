using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gradebook.Foundation.Common.Identity.Logic.Interfaces;

public interface IIdentityLogic
{
    JwtSecurityToken CreateToken(List<Claim> authClaims);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    Task EditUserRoles(string[] roles, string? userGuid = null);
    Task<ResponseWithStatus<string[], bool>> GetUserRoles(string? userGuid = null);
    Task<ResponseWithStatus<string, bool>> CurrentUserId();
    Task<ResponseWithStatus<bool>> AddUserRole(string role, string? userGuid = null);
    Task<ResponseWithStatus<bool>> RemoveUserRole(string role, string? userGuid = null);
}
