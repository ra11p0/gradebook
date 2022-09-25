using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Models.Account;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Commands.Definitions;
using Gradebook.Foundation.Common.Settings.Enums;
using Gradebook.Foundation.Common.Settings.Queries.Definitions;
using Gradebook.Foundation.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public AccountController(IServiceProvider serviceProvider)
    {
        _userManager = serviceProvider.GetResolver<UserManager<ApplicationUser>>();
        _configuration = serviceProvider.GetResolver<IConfiguration>();
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _settingsCommands = serviceProvider.GetResolver<ISettingsCommands>();
        _settingsQueries = serviceProvider.GetResolver<ISettingsQueries>();
    }

    #region authorization authentication
    [HttpGet]
    [Route("Me")]
    [ProducesResponseType(typeof(IEnumerable<PersonDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var user = await _userManager.Service.FindByNameAsync(User.Identity!.Name);
        var accessibleSchools = await _foundationQueries.Service.GetSchoolsForUser(user.Id);
        return Ok(new MeResponseModel
        {
            UserId = user.Id,
            Schools = accessibleSchools.Response ?? Enumerable.Empty<SchoolWithRelatedPersonDto>()
        });
    }
    [Authorize]
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
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        var user = await _userManager.Service.FindByNameAsync(model.Username);
        if (user != null && await _userManager.Service.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.Service.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _identityLogic.Service.CreateToken(authClaims);
            var refreshToken = _identityLogic.Service.GenerateRefreshToken();

            _ = int.TryParse(_configuration.Service["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.Service.UpdateAsync(user);

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                refresh_token = refreshToken,
                expires_in = int.Parse(_configuration.Service["JWT:TokenValidityInMinutes"]) * 60,
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.Service.FindByNameAsync(model.Email);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new LoginRegisterResponse { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email
        };
        var result = await _userManager.Service.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new LoginRegisterResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        return Ok(new LoginRegisterResponse { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    {

        string? accessToken = tokenModel.AccessToken;
        string? refreshToken = tokenModel.RefreshToken;

        var principal = _identityLogic.Service.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        string username = principal.Identity!.Name!;

        var user = await _userManager.Service.FindByNameAsync(username);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = _identityLogic.Service.CreateToken(principal.Claims.ToList());
        var newRefreshToken = _identityLogic.Service.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.Service.UpdateAsync(user);

        return new ObjectResult(new
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refresh_token = newRefreshToken,
        });
    }

    #endregion

    #region roles
    [HttpPost]
    [Route("{userGuid}/roles")]
    public async Task<IActionResult> PostRoles([FromRoute] string userGuid, [FromBody] string[] roles)
    {
        await _identityLogic.Service.EditUserRoles(roles, userGuid);
        return Ok();
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
    [HttpGet]
    [Route("{userGuid}/Settings/{settingEnum}")]
    [Authorize]
    public async Task<IActionResult> GetSetting([FromRoute] string userGuid, [FromRoute] SettingEnum settingEnum)
    {
        object? setting;
        switch (settingEnum)
        {
            case SettingEnum.DefaultPersonGuid:
                var resp = await _settingsQueries.Service.GetDefaultPersonGuid(userGuid);
                setting = resp == default ? null : resp;
                break;
            default:
                return BadRequest();
        }

        return setting is null ? NoContent() : Ok(setting);
    }
    [HttpPost]
    [Route("{userGuid}/Settings/{settingEnum}")]
    [Authorize]
    public async Task<IActionResult> SetSetting([FromRoute] string userGuid, [FromRoute] SettingEnum settingEnum, [FromBody] string jsonString)
    {
        switch (settingEnum)
        {
            case SettingEnum.DefaultPersonGuid:
                await _settingsCommands.Service.SetDefaultPersonGuid(userGuid, JsonConvert.DeserializeObject<Guid>($"\"{jsonString}\""));
                break;
            default:
                return BadRequest();
        }
        return Ok();
    }
    [HttpPost]
    [Route("{userGuid}/Settings")]
    [Authorize]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> SetSettings([FromRoute] string userGuid, [FromBody] SettingsCommand settings)
    {
        var resp = await _settingsCommands.Service.SetAccountSettings(userGuid, settings);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{userGuid}/Settings")]
    [Authorize]
    [ProducesResponseType(typeof(SettingsDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetSettings([FromRoute] string userGuid)
    {
        var resp = await _settingsQueries.Service.GetAccountSettings(userGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{userGuid}/Settings/DefaultPerson")]
    [ProducesResponseType(typeof(PersonDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [Authorize]
    public async Task<IActionResult> GetDefaultPerson([FromRoute] string userGuid)
    {
        var defaultPersonGuid = await _settingsQueries.Service.GetDefaultPersonGuid(userGuid);
        if (defaultPersonGuid == Guid.Empty)
        {
            var relatedPeople = await _foundationQueries.Service.GetSchoolsForUser(userGuid);
            return relatedPeople.Status ? Ok(relatedPeople.Response?.FirstOrDefault()?.Person) : BadRequest(relatedPeople.Message);
        }
        var personResponse = await _foundationQueries.Service.GetPersonByGuid(defaultPersonGuid);
        return personResponse.Status ? Ok(personResponse.Response) : BadRequest(personResponse.Message);
    }
    #endregion
}
