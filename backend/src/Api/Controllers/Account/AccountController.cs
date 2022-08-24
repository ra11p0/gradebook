using System.Security.Claims;
using Api.Controllers.Account.Responses;
using Api.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Gradebook.Foundation.Identity.Models;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ServiceResolver<UserManager<ApplicationUser>> _userManager;
    private readonly ServiceResolver<RoleManager<IdentityRole>> _roleManager;
    private readonly ServiceResolver<IConfiguration> _configuration;
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    public AccountController(IServiceProvider serviceProvider)
    {
        _userManager = new ServiceResolver<UserManager<ApplicationUser>>(serviceProvider);
        _roleManager = new ServiceResolver<RoleManager<IdentityRole>>(serviceProvider);
        _configuration = new ServiceResolver<IConfiguration>(serviceProvider);
        _identityLogic = new ServiceResolver<IIdentityLogic>(serviceProvider);
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

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                Username = user.UserName,
                UserId = user.Id,
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
        if (tokenModel is null)
        {
            return BadRequest("Invalid client request");
        }

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
    public async Task<IActionResult> PostRoles([FromRoute] string userGuid, [FromBody] string[] roles){
        await _identityLogic.Service.EditUserRoles(userGuid, roles);
        return Ok();
    }
    [Authorize]
    [HttpGet]
    [Route("me")]
    public async Task<IActionResult> Me(){
        var user = await _userManager.Service.FindByNameAsync(User.Identity!.Name);
        var roles = await _identityLogic.Service.GetUserRoles(user.Id);
        return Ok(new{
            user.Id,
            user.UserName,
            roles = roles.Response
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
