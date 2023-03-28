using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Implementation.Services.Exstensions
{
    public static class JwtTokenExstensions
    {
        public static string GetJwtToken(this IConfiguration config, UserEntity dto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string key = config.GetSection("JwtKey").Value;
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", dto.Id.ToString()),
                    new Claim(ClaimTypes.Email, dto.Email),
                    new Claim(ClaimTypes.GivenName, $"{dto.FirstName} {dto.LastName}"),
                    new Claim(ClaimTypes.Role, String.IsNullOrEmpty(dto.Role.Name) ? "Customer" : dto.Role.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
