using System.Net.Mail;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Identity.Models;
using Gradebook.Foundation.Identity.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Identity.Repositories;

public class CommandsRepository : BaseRepository<ApplicationIdentityDatabaseContext>, ICommandsRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    public CommandsRepository(ApplicationIdentityDatabaseContext context, UserManager<ApplicationUser> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task AssignRefreshTokenToUser(string userId, string refreshToken, int validityInDays)
    {
        var user = await Context.Users.Include(e => e.Sessions).FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null) throw new Exception("user should not be null here");
        await Context.Sessions!.AddAsync(new Session()
        {
            User = user,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = Time.UtcNow.AddDays(validityInDays)
        });
    }

    public async Task<ResponseWithStatus<string>> CreateAuthCodeForUser(string userId)
    {
        var token = new AuthorizationCode();
        var user = await Context.Users.FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null) return new ResponseWithStatus<string>(404);
        token.User = user;
        await Context.AuthorizationCodes!.AddAsync(token);
        return new ResponseWithStatus<string>(response: token.Code);
    }

    public async Task<(IdentityResult result, ApplicationUser user)> CreateUser(string email, string password)
    {
        var mailAddress = new MailAddress(email);
        ApplicationUser user = new()
        {
            Email = mailAddress.Address,
            UserName = mailAddress.User,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var res = await _userManager.CreateAsync(user, password);
        return (res, user);
    }

    public async Task RemoveRefreshTokenFromUser(string userId, string refreshToken)
    {
        var user = await Context.Users.Include(e => e.Sessions).FirstOrDefaultAsync(e => e.Id == userId);
        var session = await Context.Sessions!.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
        if (user is null || session is null) throw new Exception("user nor session should not be null here");
        user.Sessions!.Remove(session);
    }

    public async Task SetAuthorizationCodeUsed(string userId, string authCode)
    {
        var authCodeEntry = await Context.AuthorizationCodes!.FirstAsync(e =>
          e.UserId == userId &&
          e.Code == authCode &&
          !e.IsUsed &&
          e.AuthorizationCodeValidUntil > Time.UtcNow);
        authCodeEntry.IsUsed = true;
    }


}
