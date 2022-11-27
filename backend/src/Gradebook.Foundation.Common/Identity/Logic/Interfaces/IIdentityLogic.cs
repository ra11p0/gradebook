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
    Task<StatusResponse<bool>> AddUserRole(string role, string? userGuid = null);
    Task<StatusResponse<bool>> RemoveUserRole(string role, string? userGuid = null);
    Task<bool> IsValidRefreshTokenForUser(string userId, string refreshToken);
    Task RemoveRefreshTokenFromUser(string userId, string refreshToken);
    Task AssignRefreshTokenToUser(string userId, string refreshToken);
    void RemoveAllExpiredTokens();
    void SaveDatabaseChanges();
}
