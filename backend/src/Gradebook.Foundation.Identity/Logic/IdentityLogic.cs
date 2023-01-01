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
using Gradebook.Foundation.Mailservice.MailMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gradebook.Foundation.Identity.Logic;

public class IdentityLogic : IIdentityLogic
{
    private readonly ServiceResolver<IConfiguration> _configuration;
    private readonly ServiceResolver<ApplicationIdentityDatabaseContext> _identityContext;
    private readonly ServiceResolver<UserManager<ApplicationUser>> _userManager;
    private readonly ServiceResolver<RoleManager<IdentityRole>> _roleManager;
    private readonly ServiceResolver<ISettingsCommands> _settingsCommands;
    private readonly ServiceResolver<IMailClient> _mailClient;
    private readonly Context _context;
    public IdentityLogic(IServiceProvider serviceProvider)
    {
        _configuration = serviceProvider.GetResolver<IConfiguration>();
        _userManager = serviceProvider.GetResolver<UserManager<ApplicationUser>>();
        _roleManager = serviceProvider.GetResolver<RoleManager<IdentityRole>>();
        _identityContext = serviceProvider.GetResolver<ApplicationIdentityDatabaseContext>();
        _settingsCommands = serviceProvider.GetResolver<ISettingsCommands>();
        _mailClient = serviceProvider.GetResolver<IMailClient>();
        _context = serviceProvider.GetResolver<Context>().Service;
    }
    public JwtSecurityToken CreateToken(List<Claim> authClaims)
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
    public async Task EditUserRoles(string[] roles, string? userGuid = null)
    {
        if (userGuid is null) userGuid = (await CurrentUserId()).Response;
        var user = await _userManager.Service.FindByIdAsync(userGuid);
        if (user is null) return;
        var rolesToRemove = _identityContext.Service.Roles
            .Join(_identityContext.Service.UserRoles, r => r.Id, ur => ur.RoleId, (r, ur) => new { r, ur })
            .Where(e => e.ur.UserId == userGuid)
            .Where(e => !roles.Select(o => o.Normalize()).Contains(e.r.NormalizedName))
            .Select(e => e.r.Name);
        if (rolesToRemove.Any())
            await _userManager.Service.RemoveFromRolesAsync(user, rolesToRemove);
        if (roles.Any())
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.Service.RoleExistsAsync(role))
                    await _roleManager.Service.CreateAsync(new IdentityRole(role));
                await _userManager.Service.AddToRoleAsync(user, role);
            }
        }
    }
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
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
    public async Task<ResponseWithStatus<string, bool>> CurrentUserId()
    {
        var user = await _userManager.Service.FindByIdAsync(_context.UserId);
        return user is null ?
            new ResponseWithStatus<string, bool>(null, false) :
            new ResponseWithStatus<string, bool>(user.Id, true);
    }
    public async Task<ResponseWithStatus<string[], bool>> GetUserRoles(string? userGuid = null)
    {
        if (userGuid is null) userGuid = (await this.CurrentUserId()).Response;
        var response = _identityContext.Service.Roles
            .Join(_identityContext.Service.UserRoles, r => r.Id, ur => ur.RoleId, (r, ur) => new { r, ur })
            .Where(e => e.ur.UserId == userGuid)
            .Select(e => e.r.Name);
        return new ResponseWithStatus<string[], bool>(response.ToArray(), true);
    }
    public async Task<StatusResponse<bool>> AddUserRole(string role, string? userGuid = null)
    {
        await EditUserRoles((await GetUserRoles(userGuid)).Response!.Where(e => e.Normalize() != role.Normalize()).ToArray(), userGuid);
        return new StatusResponse<bool>(true);
    }
    public Task<bool> IsValidRefreshTokenForUser(string userId, string refreshToken)
    {
        return _identityContext.Service.Sessions!.AnyAsync(
            e => e.UserId == userId &&
            e.RefreshToken == refreshToken &&
            e.RefreshTokenExpiryTime >= Time.UtcNow);
    }
    public async Task RemoveRefreshTokenFromUser(string userId, string refreshToken)
    {
        var user = await _identityContext.Service.Users.Include(e => e.Sessions).FirstOrDefaultAsync(e => e.Id == userId);
        var session = await _identityContext.Service.Sessions!.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
        if (user is null || session is null) throw new Exception("user nor session should not be null here");
        user.Sessions!.Remove(session);
    }
    public async Task AssignRefreshTokenToUser(string userId, string refreshToken)
    {
        var _ = int.TryParse(_configuration.Service["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        var user = await _identityContext.Service.Users.Include(e => e.Sessions).FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null) throw new Exception("user should not be null here");
        await _identityContext.Service.Sessions!.AddAsync(new Session()
        {
            User = user,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = Time.UtcNow.AddDays(refreshTokenValidityInDays)
        });
    }
    public void SaveDatabaseChanges()
        => _identityContext.Service.SaveChanges();
    public async Task<string?> GetEmailForUser(string userId)
    {
        return (await _identityContext.Service.Users.FirstOrDefaultAsync(e => e.Id == userId))?.Email;
    }
    public async Task<StatusResponse> RegisterUser(string email, string password, string language)
    {
        _identityContext.Service.Database.BeginTransaction();
        var userExists = await _userManager.Service.FindByNameAsync(email);
        if (userExists != null)
            return new StatusResponse("User already exists");
        var authCode = new AuthorizationCode();
        ApplicationUser user = new()
        {
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = email,
            EmailConfirmed = false,
            AuthorizationCodes = new AuthorizationCode[] { authCode }
        };
        var result = await _userManager.Service.CreateAsync(user, password);
        if (!result.Succeeded) return new StatusResponse(400);
        await _settingsCommands.Service.SetLanguage(user.Id, language);
        _identityContext.Service.Database.CommitTransaction();
        await _mailClient.Service.SendMail(new ActivateAccountMailMessage(user.Id, authCode.Code));
        return new StatusResponse(true);
    }
    public async Task<ResponseWithStatus<LogInResponse>> LoginUser(string email, string password)
    {
        var user = await _userManager.Service.FindByNameAsync(email);
        if (!(user != null && await _userManager.Service.CheckPasswordAsync(user, password)))
            return new ResponseWithStatus<LogInResponse>(403);
        if (!user.EmailConfirmed)
        {
            _identityContext.Service.Database.BeginTransaction();
            var code = await CreateAuthCodeForUser(user.Id);
            if (!code.Status) return new ResponseWithStatus<LogInResponse>(code.StatusCode);
            SaveDatabaseChanges();
            _identityContext.Service.Database.CommitTransaction();
            await _mailClient.Service.SendMail(new ActivateAccountMailMessage(user.Id, code.Response!));
            return new ResponseWithStatus<LogInResponse>(302, "Email not confirmed");
        }
        var userRoles = await _userManager.Service.GetRolesAsync(user);

        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        foreach (var userRole in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));

        var token = CreateToken(authClaims);
        var refreshToken = GenerateRefreshToken();

        await AssignRefreshTokenToUser(user.Id, refreshToken);

        SaveDatabaseChanges();
        return new ResponseWithStatus<LogInResponse>(new LogInResponse()
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(token),
            refresh_token = refreshToken,
            expires_in = int.Parse(_configuration.Service["JWT:TokenValidityInMinutes"]) * 60,
        });
    }
    public async Task<ResponseWithStatus<string>> CreateAuthCodeForUser(string userId)
    {
        var token = new AuthorizationCode();
        var user = await _identityContext.Service.Users.FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null) return new ResponseWithStatus<string>(404);
        token.User = user;
        await _identityContext.Service.AuthorizationCodes!.AddAsync(token);
        SaveDatabaseChanges();
        return new ResponseWithStatus<string>(response: token.Code);
    }
    public async Task<StatusResponse> UseAuthCode(string userId, string code)
    {
        var isCodeValid = await IsAuthCodeValid(userId, code);
        if (!isCodeValid.Status) return isCodeValid;
        var authCode = await _identityContext.Service.AuthorizationCodes!.FirstAsync(e =>
            e.UserId == userId &&
            e.Code == code &&
            !e.IsUsed &&
            e.AuthorizationCodeValidUntil > Time.UtcNow);
        authCode.IsUsed = true;
        SaveDatabaseChanges();
        return new StatusResponse(true);
    }
    public async Task<StatusResponse> IsAuthCodeValid(string userId, string code)
    {
        var authCode = await _identityContext.Service.AuthorizationCodes!.FirstOrDefaultAsync(e =>
            e.UserId == userId &&
            e.Code == code &&
            !e.IsUsed &&
            e.AuthorizationCodeValidUntil > Time.UtcNow);
        if (authCode is null) return new StatusResponse(404);
        return new StatusResponse(true);
    }
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
            return new StatusResponse(message: "UnknownEmailAddress");
        var authCode = await CreateAuthCodeForUser(user.Id);
        if (!authCode.Status) return new StatusResponse(authCode.Message);
        await transaction.CommitAsync();
        return new StatusResponse(true);
    }
}
