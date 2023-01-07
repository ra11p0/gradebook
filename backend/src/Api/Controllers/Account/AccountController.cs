using System.IdentityModel.Tokens.Jwt;
using Api.Models.Account;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Commands.Definitions;
using Gradebook.Foundation.Common.Settings.Queries.Definitions;
using Gradebook.Foundation.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Account;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ServiceResolver<UserManager<ApplicationUser>> _userManager;
    private readonly ServiceResolver<IConfiguration> _configuration;
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<ISettingsQueries> _settingsQueries;
    private readonly ServiceResolver<ISettingsCommands> _settingsCommands;
    private readonly ServiceResolver<Context> _context;
    public AccountController(IServiceProvider serviceProvider)
    {
        _userManager = serviceProvider.GetResolver<UserManager<ApplicationUser>>();
        _configuration = serviceProvider.GetResolver<IConfiguration>();
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _settingsCommands = serviceProvider.GetResolver<ISettingsCommands>();
        _settingsQueries = serviceProvider.GetResolver<ISettingsQueries>();
        _context = serviceProvider.GetResolver<Context>();
    }

    #region authorization authentication
    [HttpGet]
    [Route("Me")]
    [ProducesResponseType(typeof(MeResponseModel), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var user = await _userManager.Service.FindByNameAsync(User.Identity!.Name);
        var accessibleSchools = await _foundationQueries.Service.GetSchoolsForUser(user.Id);
        if (!accessibleSchools.Status) return BadRequest();
        var schools = accessibleSchools.Response ?? Enumerable.Empty<SchoolWithRelatedPersonDto>();
        return Ok(new MeResponseModel
        {
            UserId = user.Id,
            IsActive = schools.Any(),
            Schools = schools
        });
    }
    /*[Authorize]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.Service.FindByNameAsync(username);
        if (user == null) return BadRequest("Invalid user name");

        user.RefreshToken = null;
        await _userManager.Service.UpdateAsync(user);

        return NoContent();
    }
    [Authorize]
    [HttpPost]
    [Route("revoke-all")]
    public async Task<IActionResult> RevokeAll()
    {
        var users = _userManager.Service.Users.ToList();
        foreach (var user in users)
        {
            user.RefreshToken = null;
            await _userManager.Service.UpdateAsync(user);
        }

        return NoContent();
    }*/

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        var resp = await _identityLogic.Service.LoginUser(model.Email!, model.Password!);
        return resp.ObjectResult;
    }

    [HttpPost]
    [Route("{userGuid}/emailActivation/{code}")]
    public async Task<IActionResult> Activate([FromRoute] string userGuid, [FromRoute] string code)
    {
        var res = await _identityLogic.Service.VerifyUserEmail(userGuid, code);
        return res.ObjectResult;
    }
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model, [FromQuery] string language)
    {
        if (!ModelState.IsValid) return BadRequest();
        var res = await _identityLogic.Service.RegisterUser(model.Email!, model.Password!, language);
        return res.ObjectResult;
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    {

        string? accessToken = tokenModel.AccessToken;
        string? refreshToken = tokenModel.RefreshToken;

        if (string.IsNullOrEmpty(accessToken) | string.IsNullOrEmpty(refreshToken)) return BadRequest("Invalid access token or refresh token");

        var principal = _identityLogic.Service.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        string username = principal.Identity!.Name!;

        var user = await _userManager.Service.FindByNameAsync(username);

        if (user == null ||
        !(await _identityLogic.Service.IsValidRefreshTokenForUser(user.Id, refreshToken!)))
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = _identityLogic.Service.CreateToken(principal.Claims.ToList());
        var newRefreshToken = _identityLogic.Service.GenerateRefreshToken();

        await _identityLogic.Service.RemoveRefreshTokenFromUser(user.Id, refreshToken!);
        await _identityLogic.Service.AssignRefreshTokenToUser(user.Id, newRefreshToken);

        _identityLogic.Service.SaveDatabaseChanges();

        return new ObjectResult(new
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refresh_token = newRefreshToken,
            expires_in = int.Parse(_configuration.Service["JWT:TokenValidityInMinutes"]) * 60,
        });
    }

    [HttpPost("RemindPassword")]
    public async Task<ObjectResult> RemindPassword([FromBody] string email)
    {
        return (await _identityLogic.Service.RemindPassword(email)).ObjectResult;
    }

    [HttpPost("SetNewPassword/{userId}/{authCode}/")]
    public async Task<ObjectResult> SetNewPassword([FromBody] SetNewPasswordModel model, [FromRoute] string authCode, [FromRoute] string userId)
    {
        return (await _identityLogic.Service.SetNewPassword(userId, authCode, model.Password!, model.ConfirmPassword!)).ObjectResult;
    }

    [HttpPost("SetNewPassword")]
    [Authorize]
    public async Task<ObjectResult> SetNewPasswordAuthorized([FromBody] SetNewPasswordAuthorizedModel model)
    {
        return (await _identityLogic.Service.SetNewPasswordAuthorized(model.Password!, model.ConfirmPassword!, model.OldPassword!)).ObjectResult;
    }
    #endregion

    #region roles
    [HttpPost]
    [Route("{userGuid}/roles")]
    public IActionResult PostRoles([FromRoute] string userGuid, [FromBody] string[] roles)
    {
        return NotFound();
        /*await _identityLogic.Service.EditUserRoles(roles, userGuid);
        return Ok();*/
    }
    #endregion

    #region schools
    [HttpGet]
    [Route("{userGuid}/schools")]
    [ProducesResponseType(typeof(IEnumerable<SchoolResponseModel>), 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [Authorize]
    public async Task<IActionResult> GetSchools([FromRoute] string userGuid)
    {
        var resp = await _foundationQueries.Service.GetSchoolsForUser(userGuid);
        var mappedResponse = resp.Response?.Select(response => new SchoolResponseModel()
        {
            School = response.School,
            Person = response.Person
        });
        return resp.Status ? Ok(mappedResponse) : BadRequest();
    }

    #endregion

    #region people
    [HttpGet]
    [Route("{userGuid}/people")]
    [ProducesResponseType(typeof(IEnumerable<PersonDto>), 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [Authorize]
    public async Task<IActionResult> GetRelatedPeople([FromRoute] string userGuid)
    {
        var resp = await _foundationQueries.Service.GetPeopleByUserGuid(userGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest();
    }
    #endregion

    #region settings
    [HttpPost]
    [Route("Settings")]
    [Authorize]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> SetSettings([FromBody] SettingsCommand settings)
    {
        var resp = await _settingsCommands.Service.SetAccountSettings(settings);
        return resp.ObjectResult;
    }
    [HttpGet]
    [Route("Settings")]
    [Authorize]
    [ProducesResponseType(typeof(SettingsDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetSettings()
    {
        var resp = await _settingsQueries.Service.GetAccountSettings();
        return resp.ObjectResult;
    }
    #endregion
}
