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

//dotnet ef migrations add RequiredCommentContext --startup-project ../LetterBoxd3 

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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (await _context.Users.AnyAsync(q => q.UserName == user.UserName))
        {
            return BadRequest("username already taken!");
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var targetAccount = await _context.Users.FirstOrDefaultAsync(q => q.UserName == user.UserName);
        if (targetAccount == null)
        {
            return BadRequest("Invalid username or password.");
        }

        var result = _passwordHasher.VerifyHashedPassword(targetAccount, targetAccount.PasswordHash, user.PasswordHash);
        if (result == PasswordVerificationResult.Failed)
        {
            return BadRequest("Invalid username or password.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, targetAccount.UserName),
            new Claim(ClaimTypes.NameIdentifier, targetAccount.Id.ToString())
        }),
            Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

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

    
    [HttpPost("movies/{movieId:int}/comments")]
    public async Task<IActionResult> PostComment([FromRoute] int movieId, [FromBody] Comment comment)
    {
        //don't forget to add the attributes like required to the comment modeling
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var movie = await _context.Movies
            .Include(m => m.Comments)
            .Include(m => m.Ratings)
            .FirstOrDefaultAsync(m => m.Id == movieId);
        if (movie == null)
        {
            return NotFound();
        }
        comment.MovieId = movieId;
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = movieId }, movie);
    }

    
    [HttpPatch("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> EditComment(int commentId, [FromBody] Comment newComment)
    {
        if (newComment == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
        var targetComment = await _context.Comments.FindAsync(commentId);
        if (targetComment == null)
        {
            return NotFound();
        }
        targetComment.Context = newComment.Context;
        await _context.SaveChangesAsync();
        return Ok();
    }

    
    [HttpDelete("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        var targetComment = await _context.Comments.FindAsync(commentId);
        if (targetComment == null)
        {
            return NotFound();
        }

        _context.Remove(targetComment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    
    [HttpPost("movies/{movieId:int}/rating")]
    public async Task<IActionResult> PostRating(int movieId,[FromBody] Rating rating)
    {
        if (rating == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _context.Ratings.AddAsync(rating);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { Id = movieId });
    }

    
    [HttpPatch("movies/{movieId:int}/rating/{ratingId:int}")]
    public async Task<IActionResult> editRating(int ratingId, [FromBody] Rating rating)
    {
        if (rating == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var targetRating = await _context.Ratings.FindAsync(ratingId);
        if (targetRating == null)
        {
            return NotFound();
        }

        targetRating.Score = rating.Score;
        await _context.SaveChangesAsync();

        return Ok();
    }
}
