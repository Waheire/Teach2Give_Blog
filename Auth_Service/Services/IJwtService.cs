using Auth_Service.Models;
using Auth_Service.Services.IService;
using Auth_Service.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth_Service.Services
{
    public class IJwtService : IJwt
    {
        private readonly JwtOptions _jwtOptions;

        public IJwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(ApplicationUser user, IEnumerable<string> Roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            //cred security algorithm
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //payLoad
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            //Adding a list of roles in our payload
            claims.AddRange(Roles.Select(x => new Claim(ClaimTypes.Role, x)));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
