using H3ServersideProject.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;

namespace H3ServersideProject.Data.Helpers
{
    public class TokenService
    {
        private ConfigurationManager _configuration;

        /// <summary>
        /// Method to create a token based on the user email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string CreateToken(string email)
        {
            _configuration = new ConfigurationManager();

            // sets the claims for token to use for authorize.
            List<Claim> claims = new List<Claim>
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "Logged in")
            };

            // Gets the security key that a stored in appsettings.json
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
             _configuration.AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["Token"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Create the token with the claims, an expires date and the signingCredentials.
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
