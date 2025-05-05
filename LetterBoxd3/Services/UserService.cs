using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;
using LetterBoxdContext;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LetterBoxd3.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(Context context, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (await _context.Users.AnyAsync(q => q.UserName == userDto.UserName))
                return new BadRequestObjectResult("username already taken!");

            var user = new User
            {
                UserName = userDto.UserName,
                PasswordHash = _passwordHasher.HashPassword(null, userDto.Password)
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<IActionResult> Login(UserDto userDto)
        {
            var targetAccount = await _context.Users.FirstOrDefaultAsync(q => q.UserName == userDto.UserName);
            if (targetAccount == null ||
            _passwordHasher.VerifyHashedPassword(targetAccount, targetAccount.PasswordHash, userDto.Password)
            != PasswordVerificationResult.Success)
            {
                return new BadRequestResult();
            }

            var tokenString = CreateToken(targetAccount);
            return new OkObjectResult(new { Token = tokenString });
        }
    }
}
