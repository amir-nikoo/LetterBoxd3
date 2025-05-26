    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;

//dependency injection
//getting a front end

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
        var response = await _userService.Register(userDto);
        if (!response.Success)
            return BadRequest(response.ErrorMessage);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        var response = await _userService.Login(userDto);
        if (!response.Success)
            return BadRequest(response.ErrorMessage);

        return Ok(response.Data);
    }

    [Authorize]
    [HttpGet("movies")]
    public async Task<IActionResult> GetMovies()
    {
        var response = await _movieService.GetMovies();
        return Ok(response.Data);
    }

    [Authorize]
    [HttpGet("movies/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await _movieService.GetById(id);
        if (!response.Success)
        {
            return NotFound(response.ErrorMessage);
        }

        return Ok(response.Data);
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    [Authorize]
    [HttpGet("movies/{movieId:int}/comments")]
    public async Task<IActionResult> GetComments([FromRoute] int movieId)
    {
        var response = await _commentService.GetComments(movieId);
        
        if (!response.Success)
        {
            return NotFound(response.ErrorMessage);
        }

        return Ok(response.Data);
    }

    [Authorize]
    [HttpPost("movies/{movieId:int}/comments")]
    public async Task<IActionResult> PostComment([FromRoute] int movieId, [FromBody] CommentPostDto commentPostDto)
    {
        var userId = GetCurrentUserId();
        var response = await _commentService.PostComment(movieId, userId, commentPostDto);

        if (!response.Success)
        {
            switch (response.StatusCode)
            {
                case 403:
                    return StatusCode(403, response.ErrorMessage);
                case 404:
                    return NotFound(response.ErrorMessage);
            }
        }
        return Ok(response.Data);
    }

    [Authorize]
    [HttpPatch("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> EditComment([FromRoute] int movieId, int commentId, [FromBody] CommentPostDto commentPostDto)
    {
        var userId = GetCurrentUserId();
        var response = await _commentService.EditComment(movieId, commentId, userId, commentPostDto);
        if (!response.Success)
        {
            switch (response.StatusCode)
            {
                case 403:
                    return StatusCode(403, response.ErrorMessage);
                case 404:
                    return NotFound(response.ErrorMessage);
            }
        }
        return Ok(response.Data);
    }

    [Authorize]
    [HttpDelete("movies/{movieId:int}/comments/{commentId:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int movieId, int commentId)
    {
        var userId = GetCurrentUserId();
        var response = await _commentService.DeleteComment(movieId, commentId, userId);
        if (!response.Success)
        {
            switch (response.StatusCode)
            {
                case 404:
                    return NotFound(response.ErrorMessage);
                case 403:
                    return StatusCode(403, response.ErrorMessage);
            }
        }
        return Ok(response.Data);
    }

    [Authorize]
    [HttpPost("movies/{movieId:int}/ratings")]
    public async Task<IActionResult> PostRating(int movieId,[FromBody] RatingDto ratingDto)
    {
        var userId = GetCurrentUserId();
        var response = await _ratingService.PostRating(movieId, userId, ratingDto);
        if (!response.Success)
        {
            switch (response.StatusCode)
            {
                case 403:
                    return StatusCode(403, response.ErrorMessage);
                case 404:
                    return NotFound(response.ErrorMessage);
            }
        }
        return Ok(response.Data);
    }

    [Authorize]
    [HttpPatch("movies/{movieId:int}/ratings")]
    public async Task<IActionResult> EditRating([FromRoute] int movieId, [FromBody] RatingDto ratingDto)
    {
        int userId = GetCurrentUserId();
        var response = await _ratingService.EditRating(movieId, userId, ratingDto);
        if (!response.Success)
        {
            return NotFound(response.ErrorMessage);
        }
        return Ok(response.Data);
    }
}