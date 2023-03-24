using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        public IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        

        [HttpGet]
        public IActionResult GetBearerToken(string username, string password)
        {
            var builder = new ConfigurationBuilder();
            var userRole = "";
            // Authenticate the user and generate a token
            if (IsValidUser(username, password, out userRole))
            {
                var claims = new Claim[] {};

                // Create the claims for the token
                if (userRole == "birthday")
                {
                    claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.DateOfBirth, "12/24/1993")
                    };
                }

                else if (userRole != "")
                {
                    claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, userRole)
                    };
                }

                else
                {
                    claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    };
                }

                // Create the JWT security token
                var token = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("JWT:Issuer"),
                    audience: configuration.GetValue<string>("JWT:Audience"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Secret"))),
                        SecurityAlgorithms.HmacSha256Signature));

                // Generate the encoded JWT string
                var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = encodedToken });
            }

            // Return Unauthorized if the user cannot be authenticated
            return Unauthorized();
        }

        private bool IsValidUser(string username, string password, out string userRole)
        {
            // TODO: Implement a method to validate the user's credentials
            // You can check against a database, an API, or any other data source

            // For demonstration purposes, this code only accepts a hardcoded username and password
            if (username == "admin" && password == "password")
            {
                userRole = "Admin";
                return true;
            }
            else if (username == "manager" && password == "password1")
            {
                userRole = "Manager";
                return true;
            }
            else if (username == "authenticatedUser" && password == "password2")
            {
                userRole = "";
                return true;
            }
            else if (username == "minimumAgeUser" && password == "password3")
            {
                userRole = "birthday";
                return true;
            }
            else
            {
                userRole = "";
                return false;
            }
        }
    }
}
