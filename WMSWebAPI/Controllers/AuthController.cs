using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WMSWebAPI.Dtos.Login;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto dto)
        {

            if (dto.Username != "dts" || dto.Password != "Admin1965!**")
                return Unauthorized("Credenciales inválidas");

            var issuer = "WMSWebAPI";
            var audience = "WMSWebAPIUsers";
            var key = Encoding.ASCII.GetBytes("OPaVvHGoW1WqtwoFdS0er9cC1RMrSCxd5ovsEYw22uzKlsyaO-7uOQB16jL3YnKsLB4U_BX5gWNUk0ELXMsEtg");

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, dto.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, dto.Username),
            new Claim("rol", "admin")
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}