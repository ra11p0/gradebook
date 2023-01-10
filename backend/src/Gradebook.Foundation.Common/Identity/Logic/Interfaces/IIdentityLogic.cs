using Gradebook.Foundation.Common.Identity.Responses;

namespace Gradebook.Foundation.Common.Identity.Logic.Interfaces;

public interface IIdentityLogic
{
    Task<ResponseWithStatus<string, bool>> CurrentUserId();
    Task<bool> IsValidRefreshTokenForUser(string userId, string refreshToken);
    Task RemoveRefreshTokenFromUser(string userId, string refreshToken);
    Task AssignRefreshTokenToUser(string userId, string refreshToken);
    Task<string?> GetEmailForUser(string userId);
    Task<StatusResponse> RegisterUser(string email, string password, string language);
    Task<ResponseWithStatus<LogInResponse>> LoginUser(string email, string password);
    Task<StatusResponse> VerifyUserEmail(string userId, string code);
    Task<ResponseWithStatus<string>> CreateAuthCodeForUser(string userId);
    Task<StatusResponse> UseAuthCode(string userId, string code);
    Task<StatusResponse> IsAuthCodeValid(string userId, string code);
    Task<StatusResponse> RemindPassword(string email);
    Task<StatusResponse> SetNewPassword(string userId, string authCode, string password, string confirmPassword);
    Task<StatusResponse> SetNewPasswordAuthorized(string password, string confirmPassword, string oldPassword);
    Task<ResponseWithStatus<RefreshTokenResponse>> RefreshToken(string? accessToken, string? refreshToken);
}
