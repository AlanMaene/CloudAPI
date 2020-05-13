using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Cocktail_API.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult>  Login(string idToken)
        {
            IActionResult response = Unauthorized();
            try
            {
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken, new ValidationSettings
                {
                    Audience = new[] { "998171199839-2061ud931cfaqgckitsfimod47c8nkhn.apps.googleusercontent.com" }
                });
                if (validPayload == null)
                {
                }
                else
                {
                    var tokenString = GenerateJSONWebToken(validPayload);
                    response = Ok(new { token = tokenString });
                    //return "Valid token";

                }
            }
            catch
            {
                //return "No valid Token";
            }
            return response;

            
        }

        private string GenerateJSONWebToken(Payload payload)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        //private UserModel AuthenticateUser(UserModel login)
        //{
        //    UserModel user = null;

        //    //Validate the User Credentials    
        //    //Demo Purpose, I have Passed HardCoded User Information    
        //    if (login.Username == "Jignesh")
        //    {
        //        user = new UserModel { Username = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com" };
        //    }
        //    return user;
        //}
    //}

}
}