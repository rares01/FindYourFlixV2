using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FindYourFlix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IRepository _repository;

        public LoginController(IConfiguration config, IRepository repository)
        {
            _config = config;
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User userLoginData)
        {
            var user = await SelectUser(userLoginData);

            if (user == null)
            {
                return  Unauthorized();
            }

            return Ok(new { token = GenerateJwt(user) });
        }

        private string GenerateJwt(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sid, userInfo.Id),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> SelectUser(User userLoginData)
        {
            var query = _repository.Query<User>().Where(e => e.UserName == userLoginData.UserName);

            if (await query.AnyAsync())
            {
                var savedPasswordHash = await query.Select(e => e.Password).FirstOrDefaultAsync();
                if (!CheckPassword(userLoginData.Password, savedPasswordHash))
                {
                    throw new UnauthorizedAccessException();
                }
            }
            else
                return null;

            return await query.Select(e => new User
            {
                Id = e.Id,
                UserName = e.UserName,
                Email = e.Email,
                IsAdmin = e.IsAdmin
            }).FirstOrDefaultAsync();
        }

        private static bool CheckPassword(string insertedPassword, string savedPasswordHash)
        {
            var hashBytes = Convert.FromBase64String(savedPasswordHash);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(insertedPassword, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            for (var i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
    }
}