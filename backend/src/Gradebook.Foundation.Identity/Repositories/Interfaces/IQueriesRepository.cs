using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Identity.Repositories.Interfaces;

public interface IQueriesRepository : IBaseRepository
{
    Task<bool> IsValidRefreshTokenForUser(string userId, string refreshToken);
    Task<string?> GetEmailForUser(string userId);
    Task<bool> AnyUserWithEmail(string email);
    Task<string?> GetUserIdByUserEmail(string email);
    Task<StatusResponse> IsAuthCodeValid(string userId, string code);
    Task<bool> IsPasswordValidForUser(string userId, string password);
    Task<bool> UserHasEmailConfirmed(string userId);
    Task<string> GetUserUsername(string userId);
}
