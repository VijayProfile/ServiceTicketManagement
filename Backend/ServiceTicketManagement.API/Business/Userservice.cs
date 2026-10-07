using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceTicketManagement.API.Data;
using ServiceTicketManagement.API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceTicketManagement.API.Business
{
    public class Userservice
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public Userservice(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #region SignUp
        public async Task<string> CreateAccount(SignUp signUp)
        {
            var account = new Users
            {
                UserName = signUp.UserName,
                Password = signUp.Password,
            };

            await _context.AddAsync(account);
            await _context.SaveChangesAsync();
            return "Account Created Successfully";
        }
        #endregion

        #region Login
        public async Task<string?> JWTLogin(Login login)
        {
            // Normally check username/password from database
            var user = await _context.Users
                               .FirstOrDefaultAsync(p => p.UserName == login.UserName
                                                    && p.Password == login.Password);



            if (user == null)
            {
                return null;
            }

            var claims = new[]
            {
           new Claim(ClaimTypes.Name, user.UserName)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
             .WriteToken(token);

            return tokenString;

        }
        #endregion
    }
}
