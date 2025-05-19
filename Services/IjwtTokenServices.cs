using Microsoft.IdentityModel.Tokens;
using MovieApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApplication.Services
{
    public interface IjwtTokenServices
    {
        public string GenerateToken(ApplicationUser user, IList<string> roles);
    }

    public class JwtTokenServices : IjwtTokenServices
    {
      
        private readonly IConfiguration _configuration;

        public JwtTokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       

            public string GenerateToken(ApplicationUser user, IList<string> roles)
            {
                var jwtSetting = _configuration.GetSection("JwtSetting");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtSetting["Issuer"],
                    audience: jwtSetting["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSetting["ExpiryInMinutes"])),
                    signingCredentials: creds
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
    }

}
