using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WMSWebAPI.Services.Login_JWT
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string usuario)
        {
            var jwtKey = _configuration["Jwt:Key"]
                ?? "OPaVvHGoW1WqtwoFdS0er9cC1RMrSCxd5ovsEYw22uzKlsyaO-7uOQB16jL3YnKsLB4U_BX5gWNUk0ELXMsEtg";

            var issuer = _configuration["Jwt:Issuer"] ?? "WMSWebAPI";
            var audience = _configuration["Jwt:Audience"] ?? "WMSWebAPIUsers";

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario),
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario),
            new Claim(ClaimTypes.Name, usuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
