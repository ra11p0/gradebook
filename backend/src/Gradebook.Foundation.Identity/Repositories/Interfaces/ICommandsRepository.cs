using Gradebook.Foundation.Common;
using Gradebook.Foundation.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Gradebook.Foundation.Identity.Repositories.Interfaces;

public interface ICommandsRepository : IBaseRepository
{
    Task RemoveRefreshTokenFromUser(string userId, string refreshToken);
    Task AssignRefreshTokenToUser(string userId, string refreshToken, int validityInDays);
    Task<ResponseWithStatus<string>> CreateAuthCodeForUser(string userId);
    Task<(IdentityResult result, ApplicationUser user)> CreateUser(string email, string password);
    Task SetAuthorizationCodeUsed(string userId, string authCode);
}
