using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Controllers.Account.Responses;
using Api.Models.Account;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
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
    //private readonly ServiceResolver<RoleManager<IdentityRole>> _roleManager;
    private readonly ServiceResolver<IConfiguration> _configuration;
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public AccountController(IServiceProvider serviceProvider)
    {
        _userManager = serviceProvider.GetResolver<UserManager<ApplicationUser>>();
        //_roleManager = serviceProvider.GetResolver<RoleManager<IdentityRole>>();
        _configuration = serviceProvider.GetResolver<IConfiguration>();
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
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

            var roles = await _userManager.Service.GetRolesAsync(user);
            var personGuid = await _foundationQueries.Service.GetPersonGuidForUser(user.Id);
            var person = await _foundationQueries.Service.GetPersonByGuid(personGuid.Response);

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                refresh_token = refreshToken,
                Expiration = token.ValidTo,
                expires_in = int.Parse(_configuration.Service["JWT:TokenValidityInMinutes"]) * 60,
                Username = user.UserName,
                UserId = user.Id,
                PersonGuid = personGuid.Response,
                person.Response.Name,
                person.Response.Surname,
                Roles = roles
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
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }
    [Authorize(Roles = "SuperAdmin")]
    [HttpPost]
    [Route("{userGuid}/roles")]
    public async Task<IActionResult> PostRoles([FromRoute] string userGuid, [FromBody] string[] roles)
    {
        await _identityLogic.Service.EditUserRoles(roles, userGuid);
        return Ok();
    }
    [HttpGet]
    [Route("me")]
    [Authorize]
    [ProducesResponseType(typeof(MeResponse), 200)]
    public async Task<IActionResult> Me()
    {
        var user = await _userManager.Service.FindByNameAsync(User.Identity!.Name);
        var roles = await _identityLogic.Service.GetUserRoles(user.Id);
        var personGuid = await _foundationQueries.Service.GetCurrentPersonGuid();
        var person = await _foundationQueries.Service.GetPersonByGuid(personGuid.Response);
        return Ok(new MeResponse
        {
            Id = user.Id,
            Username = user.UserName,
            PersonGuid = personGuid.Response,
            Roles = roles.Response,
            Name = person.Response?.Name,
            Surname = person.Response?.Surname,
            Birthday = person.Response?.Birthday,
            SchoolRole = person.Response?.SchoolRole,
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
}
