using LetterBoxdContext;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LetterBoxd3.Configurations.Dtos;

//authorization problem in swagger

[Route("api")]
[ApiController]
public class Controller : ControllerBase
{
    private readonly Context _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;
    public Controller(Context context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    private string CreateToken(User user)
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


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (await _context.Users.AnyAsync(q => q.UserName == userDto.UserName))
        {
            return BadRequest("username already taken!");
        }

        var user = new User
        {
            UserName = userDto.UserName,
            PasswordHash = _passwordHasher.HashPassword(null, userDto.Password)
        };

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        var targetAccount = await _context.Users.FirstOrDefaultAsync(q => q.UserName == userDto.UserName);
        if (targetAccount == null ||
        _passwordHasher.VerifyHashedPassword(targetAccount, targetAccount.PasswordHash, userDto.Password)
        != PasswordVerificationResult.Success)
        {
            return BadRequest("Invalid username or password.");
        }

        var tokenString = CreateToken(targetAccount);
        return Ok(new { Token = tokenString });
    }

    
    [HttpGet("movies")]
    public async Task<IActionResult> GetMovies()
    {
        try
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("movies/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var targetMovie = await _context.Movies
            .Include(m => m.Ratings)
            .Include(m => m.Comments)
            .FirstOrDefaultAsync(j => j.Id == id);
        if (targetMovie == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(targetMovie);
        }
    }

    [Authorize]
    [HttpPost("movies/{movieId:int}/comments")]
    public async Task<IActionResult> PostComment([FromRoute] int movieId, [FromBody] CommentDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var movie = await _context.Movies.FindAsync(movieId);
        if (movie == null)
        {
            return NotFound();
        }
        var newComment = new Comment
        {
            MovieId = movieId,
            Context = commentDto.Context
        };

        await _context.AddAsync(newComment);
        await _context.SaveChangesAsync();
        return Created($"movies/{movieId}/comments/{newComment.Id}", newComment);
    }

    [Authorize]
    [HttpPatch("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> EditComment(int commentId, [FromBody] CommentDto commentDto)
    {
        var targetComment = await _context.Comments.FindAsync(commentId);
        if (targetComment == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (targetComment.UserId != int.Parse(userId))
            return Forbid();

        if (commentDto.Context == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
        
        targetComment.Context = commentDto.Context;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [Authorize]
    [HttpDelete("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        var targetComment = await _context.Comments.FindAsync(commentId);
        if (targetComment == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (targetComment.UserId != int.Parse(userId))
            return Forbid();

        _context.Remove(targetComment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpPost("movies/{movieId:int}/rating")]
    public async Task<IActionResult> PostRating(int movieId,[FromBody] RatingDto ratingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newRating = new Rating
        {
            MovieId = movieId,
            Score = ratingDto.Score
        };

        await _context.Ratings.AddAsync(newRating);
        await _context.SaveChangesAsync();
        return Created($"movies/{movieId}/rating/{newRating.Id}", newRating);
    }

    [Authorize]
    [HttpPatch("movies/{movieId:int}/rating/{ratingId:int}")]
    public async Task<IActionResult> EditRating(int ratingId, [FromBody] RatingDto ratingDto)
    {
        var targetRating = await _context.Ratings.FindAsync(ratingId);
        if (targetRating == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (targetRating.UserId != int.Parse(userId))
            return Forbid();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        targetRating.Score = ratingDto.Score;
        await _context.SaveChangesAsync();
        return Ok();
    }
}
