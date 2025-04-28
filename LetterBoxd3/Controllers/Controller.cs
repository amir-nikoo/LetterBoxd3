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
using LetterBoxd3.Dtos;
using System.ComponentModel;

//to git the commentDto changes and added Comment a User navigation propertt

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
            return BadRequest(ModelState);
        
        if (await _context.Users.AnyAsync(q => q.UserName == userDto.UserName))
            return BadRequest("username already taken!");

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
        var movies = await _context.Movies
        .Select(m => new
        {
            m.Id,
            m.ImageUrl,
            m.Title
        })
        .ToListAsync();
        return Ok(movies);
    }


    [HttpGet("movies/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var targetMovie = await GetMovieWithDetails(id);

        if (targetMovie == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(targetMovie);
        }
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    private async Task<MovieDto> GetMovieWithDetails(int movieId)
    {
        var movie = await _context.Movies
        .Include(m => m.Comments)
            .ThenInclude(c => c.User)
        .Include(m => m.Ratings)
        .FirstOrDefaultAsync(m => m.Id == movieId);

        return new MovieDto
        {
            Id = movie.Id,
            ImageUrl = movie.ImageUrl,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            Description = movie.Description,
            Ratings = movie.Ratings,
            Comments = movie.Comments.Select(c => new CommentGetDto
            {
                Id = c.Id,
                Text = c.Text,
                Username = c.User?.UserName ?? "Deleted"
            }).ToList()
        };
    }

    
    [HttpPost("movies/{movieId:int}/comments")]
    public async Task<IActionResult> PostComment([FromRoute] int movieId, [FromBody] CommentPostDto commentPostDto)
    {
        var userId = GetCurrentUserId();
        var commentExists = await _context.Comments.AnyAsync(c => c.MovieId == movieId && c.UserId == userId);
        if (commentExists)
            return Forbid();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var movie = await _context.Movies.FindAsync(movieId);
        if (movie == null)
            return NotFound();

        var newComment = new Comment
        {
            MovieId = movieId,
            Text = commentPostDto.Text,
            UserId = userId
        };

        await _context.AddAsync(newComment);
        await _context.SaveChangesAsync();

        return Ok(await GetMovieWithDetails(movieId));  
    }

    
    [HttpPatch("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> EditComment([FromRoute] int movieId, int commentId, [FromBody] CommentPostDto commentPostDto)
    {
        var targetComment = await _context.Comments.FindAsync(commentId);
        if (targetComment == null)
            return NotFound();

        if (targetComment.UserId != GetCurrentUserId())
            return Forbid();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        targetComment.Text = commentPostDto.Text;
        await _context.SaveChangesAsync();

        return Ok(await GetMovieWithDetails(movieId));
    }

    
    [HttpDelete("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int movieId, int commentId)
    {
        var targetComment = await _context.Comments.FindAsync(commentId);
        if (targetComment == null)
            return NotFound();

        if (targetComment.UserId != GetCurrentUserId())
            return Forbid();

        _context.Remove(targetComment);
        await _context.SaveChangesAsync();

        return Ok(await GetMovieWithDetails(movieId));
    }

    
    [HttpPost("movies/{movieId:int}/rating")]
    public async Task<IActionResult> PostRating(int movieId,[FromBody] RatingDto ratingDto)
    {
        var userId = GetCurrentUserId();
        var ratingExists = await _context.Ratings.AnyAsync(r => r.MovieId == movieId && r.UserId == userId);

        if (ratingExists)
            return Forbid();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var movie = await _context.Movies.FindAsync(movieId);
        if (movie == null)
            return NotFound();

        var newRating = new Rating
        {
            MovieId = movieId,
            Score = ratingDto.Score,
            UserId = userId
        };

        await _context.Ratings.AddAsync(newRating);
        await _context.SaveChangesAsync();

        return Ok(await GetMovieWithDetails(movieId));
    }

    
    [HttpPatch("movies/{movieId:int}/rating/{ratingId:int}")]
    public async Task<IActionResult> EditRating([FromRoute] int movieId, int ratingId, [FromBody] RatingDto ratingDto)
    {
        var targetRating = await _context.Ratings.FindAsync(ratingId);
        if (targetRating == null)
            return NotFound();

        if (targetRating.UserId != GetCurrentUserId())
            return Forbid();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        targetRating.Score = ratingDto.Score;
        await _context.SaveChangesAsync();

        return Ok(await GetMovieWithDetails(movieId));
    }
}
