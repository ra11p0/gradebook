using Gradebook.Foundation.Common;
using Gradebook.Foundation.Identity.Models;
using Gradebook.Foundation.Identity.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Identity.Repositories;

public class QueriesRepository : BaseRepository<ApplicationIdentityDatabaseContext>, IQueriesRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    public QueriesRepository(ApplicationIdentityDatabaseContext context, UserManager<ApplicationUser> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task<bool> AnyUserWithEmail(string email)
    {
        var user = await _userManager.FindByNameAsync(email);
        return user is not null;
    }

    public async Task<string?> GetEmailForUser(string userId)
        => (await Context.Users.FirstOrDefaultAsync(e => e.Id == userId))?.Email;

    public async Task<string?> GetUserIdByUserEmail(string email)
    {
        return (await _userManager.FindByEmailAsync(email))?.Id;
    }

    public async Task<string> GetUserUsername(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user.UserName;
    }

    public async Task<StatusResponse> IsAuthCodeValid(string userId, string code)
    {
        var authCode = await Context.AuthorizationCodes!.FirstOrDefaultAsync(e =>
            e.UserId == userId &&
            e.Code == code &&
            !e.IsUsed &&
            e.AuthorizationCodeValidUntil > Time.UtcNow);
        if (authCode is null) return new StatusResponse(404);
        return new StatusResponse(true);
    }

    public async Task<bool> IsPasswordValidForUser(string userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return (user != null && await _userManager.CheckPasswordAsync(user, password));
    }

    public Task<bool> IsValidRefreshTokenForUser(string userId, string refreshToken)
    {
        return Context.Sessions!.AnyAsync(
           e => e.UserId == userId &&
           e.RefreshToken == refreshToken &&
           e.RefreshTokenExpiryTime >= Time.UtcNow);
    }

    public async Task<bool> UserHasEmailConfirmed(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user.EmailConfirmed;
    }
}
