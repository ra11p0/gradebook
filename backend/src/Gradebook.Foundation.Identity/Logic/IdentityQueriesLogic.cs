using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Gradebook.Foundation.Common.Identity.Logic.Queries.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Gradebook.Foundation.Identity.Logic;

public class IdentityQueriesLogic : IIdentityQueriesLogic
{
    public JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        /*var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;*/
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        throw new NotImplementedException();
    }
}
