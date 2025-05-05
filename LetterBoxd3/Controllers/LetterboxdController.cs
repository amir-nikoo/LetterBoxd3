using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;

//dar hal e ezafe kardan e word filtering hastam

[Route("api")]
[ApiController]
public class LetterboxdController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMovieService _movieService;
    private readonly ICommentService _commentService;
    private readonly IRatingService _ratingService;
    public LetterboxdController(IUserService userService, IMovieService movieService, ICommentService commentService, IRatingService ratingService)
    {
        _userService = userService;
        _movieService = movieService;
        _commentService = commentService;
        _ratingService = ratingService;
        
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        return await _userService.Register(userDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        return await _userService.Login(userDto);
    }

    [Authorize]
    [HttpGet("movies")]
    public async Task<IActionResult> GetMovies()
    {
        return await _movieService.GetMovies();
    }

    [Authorize]
    [HttpGet("movies/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        return await _movieService.GetById(id);
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    [Authorize]
    [HttpPost("movies/{movieId:int}/comments")]
    public async Task<IActionResult> PostComment([FromRoute] int movieId, [FromBody] CommentPostDto commentPostDto)
    {
        var userId = GetCurrentUserId();
        return await _commentService.PostComment(movieId, userId, commentPostDto);
    }

    [Authorize]
    [HttpPatch("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> EditComment([FromRoute] int movieId, int commentId, [FromBody] CommentPostDto commentPostDto)
    {
        var userId = GetCurrentUserId();
        return await _commentService.EditComment(movieId, commentId, userId, commentPostDto);
    }

    [Authorize]
    [HttpDelete("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int movieId, int commentId)
    {
        var userId = GetCurrentUserId();
        return await _commentService.DeleteComment(movieId, commentId, userId);
    }

    [Authorize]
    [HttpPost("movies/{movieId:int}/rating")]
    public async Task<IActionResult> PostRating(int movieId,[FromBody] RatingDto ratingDto)
    {
        var userId = GetCurrentUserId();
        return await _ratingService.PostRating(movieId, userId, ratingDto);
    }

    [Authorize]
    [HttpPatch("movies/{movieId:int}/rating/{ratingId:int}")]
    public async Task<IActionResult> EditRating([FromRoute] int movieId, int ratingId, [FromBody] RatingDto ratingDto)
    {
        int userId = GetCurrentUserId();
        return await _ratingService.EditRating(movieId, userId, ratingId, ratingDto);
    }
}
