using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Config;
using ToDoList.interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    /// <summary>
    /// Service for generating JWT tokens.
    /// </summary>
    public class TokenService(IOptions<JwtConfigs> _configs) : ITokenService
    {
        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is to be created.</param>
        /// <returns>A JWT token as a string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when token creation fails.</exception>
        /// <remarks>
        /// This method generates a JWT token using the provided user information and configuration settings.
        /// It includes claims for the user's ID and sets the token's expiration time based on configuration.
        /// </remarks>
        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs.Value.Secret)), SecurityAlgorithms.HmacSha256)
            {

            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                ]),
                Expires = DateTime.UtcNow.AddHours(_configs.Value.ExpirationInHour),
                SigningCredentials = credentials,
                Issuer = _configs.Value.Issuer,
                Audience = _configs.Value.Audience

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}