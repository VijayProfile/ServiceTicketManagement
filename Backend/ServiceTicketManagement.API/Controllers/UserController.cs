using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceTicketManagement.API.Business;
using ServiceTicketManagement.API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceTicketManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Userservice _userservice;
      
        public UserController(Userservice userservice)
        {
            _userservice= userservice;
            
        }

        #region Signup
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUp signUp) 
        {
            var result = await _userservice.CreateAccount(signUp);

            if (result is null) 
            {
                return BadRequest(new 
                {
                    message="Something wrong"
                });
            }
            return Ok(new { message=result});
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await _userservice.JWTLogin(login);

            if (result is null)
            {
                return Unauthorized(new
                {
                    message = "Unauthorized User"
                });
            }
            return Ok(new
            {
                message = "Login successful",
                token = result 
            });
        }
        #endregion
    }
}
