using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RetardCheck.Auth.Options;

namespace API.Auth;

public class JwtService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string CreateToken(ClaimsPrincipal principal)
    {
        var now = DateTime.Now;

        var jwt = new JwtSecurityToken(
            notBefore: now,
            expires: now.AddMilliseconds(_options.TokenLifetime),
            claims: principal.Claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey)),
                SecurityAlgorithms.HmacSha256
            )
        );

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }
}