using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductAssemblySystem.UserManagement.Domain.Entities;
using ProductAssemblySystem.UserManagement.Infrastructure.Authentication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductAssemblySystem.UserManagement.Infrastructure.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public string GenerateJwtSecurityToken(User user)
        {
            Claim[] claims = [new(CustomClaims.UserId, user.Id.ToString())];

            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));

            string jwtSecurityTokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return jwtSecurityTokenString;
        }
    }
}
