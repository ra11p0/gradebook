using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Common.Identity.Responses;
using Gradebook.Foundation.Common.Mailservice;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Identity.Models;
using Gradebook.Foundation.Identity.Repositories.Interfaces;
using Gradebook.Foundation.Mailservice.MailMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gradebook.Foundation.Identity.Logic;

public class IdentityLogic : IIdentityLogic
{
    private readonly ServiceResolver<IConfiguration> _configuration;
    private readonly ServiceResolver<ApplicationIdentityDatabaseContext> _identityContext;
    private readonly ServiceResolver<UserManager<ApplicationUser>> _userManager;
    private readonly ServiceResolver<ISettingsCommands> _settingsCommands;
    private readonly ServiceResolver<IMailClient> _mailClient;
    private readonly ServiceResolver<IQueriesRepository> _queriesRepository;
    private readonly ServiceResolver<ICommandsRepository> _commandsRepository;
    private readonly Context _context;
    public IdentityLogic(IServiceProvider serviceProvider)
    {
        _configuration = serviceProvider.GetResolver<IConfiguration>();
        _userManager = serviceProvider.GetResolver<UserManager<ApplicationUser>>();
        _identityContext = serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>();
        _settingsCommands = serviceProvider.GetResolver<ISettingsCommands>();
        _mailClient = serviceProvider.GetResolver<IMailClient>();
        _context = serviceProvider.GetResolver<Context>().Service;
        _commandsRepository = serviceProvider.GetResolver<ICommandsRepository>();
        _queriesRepository = serviceProvider.GetResolver<IQueriesRepository>();
    }
    public async Task<ResponseWithStatus<string, bool>> CurrentUserId()
    {
        var user = await _userManager.Service.FindByIdAsync(_context.UserId);
        return user is null ?
            new ResponseWithStatus<string, bool>(null, false) :
            new ResponseWithStatus<string, bool>(user.Id, true);
    }
    public Task<bool> IsValidRefreshTokenForUser(string userId, string refreshToken)
        => _queriesRepository.Service.IsValidRefreshTokenForUser(userId, refreshToken);
    public async Task RemoveRefreshTokenFromUser(string userId, string refreshToken)
    {
        await _commandsRepository.Service.RemoveRefreshTokenFromUser(userId, refreshToken);
        await _commandsRepository.Service.SaveChangesAsync();
    }
    public async Task AssignRefreshTokenToUser(string userId, string refreshToken)
    {
        var refreshTokenValidityInDays = int.Parse(_configuration.Service["JWT:RefreshTokenValidityInDays"]);
        await _commandsRepository.Service.AssignRefreshTokenToUser(userId, refreshToken, refreshTokenValidityInDays);
        await _commandsRepository.Service.SaveChangesAsync();
    }
    public void SaveDatabaseChanges()
        => _identityContext.Service.SaveChanges();
    public Task<string?> GetEmailForUser(string userId)
        => _queriesRepository.Service.GetEmailForUser(userId);
    public async Task<StatusResponse> RegisterUser(string email, string password, string language)
    {
        if (await _queriesRepository.Service.AnyUserWithEmail(email))
            return new StatusResponse("User already exists");

        using (var t = _commandsRepository.Service.BeginTransaction())
        {
            var result = await _commandsRepository.Service.CreateUser(email, password);
            if (!result.Succeeded) return new StatusResponse(400);

            var userId = await _queriesRepository.Service.GetUserIdByUserEmail(email);
            if (string.IsNullOrEmpty(userId)) return new StatusResponse(400);

            await SendAccountActivationEmail(userId);
            await SetUserDefaultSettings(userId, language);
            await _commandsRepository.Service.SaveChangesAsync();
            await t.CommitAsync();
        }

        return new StatusResponse(true);
    }
    public async Task<ResponseWithStatus<LogInResponse>> LoginUser(string email, string password)
    {
        if (!(await _queriesRepository.Service.AnyUserWithEmail(email)))
            return new ResponseWithStatus<LogInResponse>(404, "User not found");

        var userId = await _queriesRepository.Service.GetUserIdByUserEmail(email);
        if (string.IsNullOrEmpty(userId))
            return new ResponseWithStatus<LogInResponse>(404, "User not found");

        if (!(await _queriesRepository.Service.IsPasswordValidForUser(userId!, password)))
            return new ResponseWithStatus<LogInResponse>(403, "Invalid login or password");

        if (!(await _queriesRepository.Service.UserHasEmailConfirmed(userId)))
        {
            await SendAccountActivationEmail(userId);
            return new ResponseWithStatus<LogInResponse>(302, "Email not confirmed");
        }

        var tokenData = await PrepareAccessToken(userId);
        return new ResponseWithStatus<LogInResponse>(new LogInResponse()
        {
            access_token = tokenData.accessToken,
            refresh_token = tokenData.refreshToken,
            expires_in = tokenData.expiresIn,
        });
    }
    public async Task<ResponseWithStatus<string>> CreateAuthCodeForUser(string userId)
    {
        var resp = await _commandsRepository.Service.CreateAuthCodeForUser(userId);
        await _commandsRepository.Service.SaveChangesAsync();
        return resp;
    }
    public async Task<StatusResponse> UseAuthCode(string userId, string code)
    {
        var isCodeValid = await IsAuthCodeValid(userId, code);
        if (!isCodeValid.Status) return isCodeValid;
        await _commandsRepository.Service.SetAuthorizationCodeUsed(userId, code);
        await _commandsRepository.Service.SaveChangesAsync();
        return new StatusResponse(true);
    }
    public Task<StatusResponse> IsAuthCodeValid(string userId, string code)
        => _queriesRepository.Service.IsAuthCodeValid(userId, code);
    public async Task<StatusResponse> VerifyUserEmail(string userId, string code)
    {
        var useAuthCode = await UseAuthCode(userId, code);
        if (!useAuthCode.Status) return useAuthCode;
        var user = await _userManager.Service.FindByIdAsync(userId);
        if (user is null) return new StatusResponse(404);
        user.EmailConfirmed = true;
        await _userManager.Service.UpdateAsync(user);
        return new StatusResponse(true);
    }
    public async Task<StatusResponse> RemindPassword(string email)
    {
        using var transaction = await _identityContext.Service.Database.BeginTransactionAsync();
        var user = await _userManager.Service.FindByEmailAsync(email);
        if (user is null)
            return new StatusResponse(404, message: "UnknownEmailAddress");
        var authCode = await CreateAuthCodeForUser(user.Id);
        if (!authCode.Status) return new StatusResponse(authCode.Message);
        await _mailClient.Service.SendMail(new RemindPasswordMailMessage(user.Id, authCode.Response!));
        await transaction.CommitAsync();
        return new StatusResponse(true);
    }
    public async Task<StatusResponse> SetNewPassword(string userId, string authCode, string password, string confirmPassword)
    {
        if (password != confirmPassword) return new StatusResponse("PasswordsNotTheSame");
        using var transaction = await _identityContext.Service.Database.BeginTransactionAsync();
        var useAuthResp = await UseAuthCode(userId, authCode);
        if (!useAuthResp.Status) return useAuthResp;
        var user = await _userManager.Service.FindByIdAsync(userId);
        if (user is null) return new StatusResponse(404);
        await _userManager.Service.RemovePasswordAsync(user);
        await _userManager.Service.AddPasswordAsync(user, password);
        await transaction.CommitAsync();
        return new StatusResponse(true);
    }
    public async Task<StatusResponse> SetNewPasswordAuthorized(string password, string confirmPassword, string oldPassword)
    {
        if (password != confirmPassword) return new StatusResponse("PasswordsNotTheSame");
        var user = await _userManager.Service.FindByIdAsync(_context.UserId);
        if (!(user != null && await _userManager.Service.CheckPasswordAsync(user, oldPassword)))
            return new StatusResponse(403);
        await _userManager.Service.RemovePasswordAsync(user);
        await _userManager.Service.AddPasswordAsync(user, password);
        return new StatusResponse(200);
    }
    public async Task<ResponseWithStatus<RefreshTokenResponse>> RefreshToken(string? accessToken, string? refreshToken)
    {
        if (string.IsNullOrEmpty(accessToken) | string.IsNullOrEmpty(refreshToken)) return new ResponseWithStatus<RefreshTokenResponse>("Invalid access token or refresh token");

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
            return new ResponseWithStatus<RefreshTokenResponse>("Invalid access token or refresh token");

        string username = principal.Identity!.Name!;

        var user = await _userManager.Service.FindByNameAsync(username);
        var newAccessToken = CreateAccessToken(principal.Claims.ToList());
        var newRefreshToken = CreateRefreshToken();
        using (var t = await _identityContext.Service.Database.BeginTransactionAsync())
        {
            if (user == null ||
            !(await IsValidRefreshTokenForUser(user.Id, refreshToken!)))
            {
                await t.RollbackAsync();
                return new ResponseWithStatus<RefreshTokenResponse>("Invalid access token or refresh token");
            }
            await AssignRefreshTokenToUser(user.Id, newRefreshToken);
            SaveDatabaseChanges();
            await t.CommitAsync();
        }

        return new ResponseWithStatus<RefreshTokenResponse>(new RefreshTokenResponse()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
            ExpiresIn = int.Parse(_configuration.Service["JWT:TokenValidityInMinutes"]) * 60,
        });
    }

    #region private
    private JwtSecurityToken CreateAccessToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Service["JWT:Secret"]));
        var _ = int.TryParse(_configuration.Service["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration.Service["JWT:ValidIssuer"],
            audience: _configuration.Service["JWT:ValidAudience"],
            expires: Time.UtcNow.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
    private async Task<(string accessToken, string refreshToken, int expiresIn)> PrepareAccessToken(string userId)
    {
        var username = await _queriesRepository.Service.GetUserUsername(userId);
        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        var token = CreateAccessToken(authClaims);
        var refreshToken = CreateRefreshToken();

        await AssignRefreshTokenToUser(userId, refreshToken);

        return (new JwtSecurityTokenHandler().WriteToken(token), refreshToken, int.Parse(_configuration.Service["JWT:TokenValidityInMinutes"]) * 60);
    }
    private string CreateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Service["JWT:Secret"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
    private async Task SendAccountActivationEmail(string userId)
    {
        var auth = await CreateAuthCodeForUser(userId);

        await _mailClient.Service.SendMail(new ActivateAccountMailMessage(userId, auth.Response!));
    }
    private async Task SetUserDefaultSettings(string userId, string language)
    {
        await _settingsCommands.Service.SetLanguage(userId, language);
    }
    #endregion
}
