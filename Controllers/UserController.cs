using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineAptitudeTestDB.Dto.User;
using OnlineAptitudeTestDB.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineAptitudeTestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly OnlineAptitudeTestDbContext _context;
        private readonly IConfiguration _config;
        public UserController(OnlineAptitudeTestDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("candidateLogin")]
        public IActionResult CandidateLogin(LoginRequest userLogin)
        {
            var user = _context.Candidates.Where(u => u.Username == userLogin.UserName)
               .FirstOrDefault();
            if (user == null)
                return Unauthorized();            
            if (user.Password != userLogin.Password)
                return Unauthorized();
            return Ok(new UserData { Id = user.CandidateId, Name = user.Name, UserName = user.Username, Token = GenerateJWTCandidate(user) });
        }

        [HttpPost("adminLogin")]
        public IActionResult AdminLogin(LoginRequest userLogin)
        {
            var user = _context.AdminManagers.Where(u => u.Username == userLogin.UserName)
               .FirstOrDefault();
            if (user == null)
                return Unauthorized("abc");
            if (user.Password != userLogin.Password)
                return Unauthorized("abc");

            return Ok(new UserData { Id = user.AdminManagerId, Name = user.Name, UserName = user.Username, Token = GenerateJWTAdmin(user) });
        }

        private string GenerateJWTAdmin(AdminManager user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signatureKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.AdminManagerId.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Username",user.Username),
                new Claim(ClaimTypes.Role,user.Role),
            };
            var token = new JwtSecurityToken(
                _config["JWT:Issuer"],  
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signatureKey
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private String GenerateJWTCandidate(Candidate user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signatureKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.CandidateId.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Username",user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(
                _config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signatureKey
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
