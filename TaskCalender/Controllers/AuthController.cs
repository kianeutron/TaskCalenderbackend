using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskCalender.Data;
using TaskCalender.Models.Auth;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace TaskCalender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TaskCalenderContext _context;
        private readonly IConfiguration _configuration;
        private const string JwtSecret = "THIS_IS_A_DEMO_SECRET_CHANGE_ME_1234567890"; // For demo only

        public AuthController(TaskCalenderContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Principals.FirstOrDefault(u => u.pcl_UserName == request.UserName && u.pcl_Password == request.Password);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.pcl_Id.ToString()),
                    new Claim(ClaimTypes.Name, user.pcl_UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new LoginResponse
            {
                Token = tokenString,
                UserName = user.pcl_UserName,
                UserId = user.pcl_Id
            });
        }
    }
} 