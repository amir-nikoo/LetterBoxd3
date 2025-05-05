using LetterBoxd3.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface ICommentService
    {
        Task<IActionResult> PostComment([FromRoute] int movieId, int userId, CommentPostDto commentPostDto);
        Task<IActionResult> EditComment([FromRoute] int movieId, int commentId, int userId, CommentPostDto commentPostDto);
        Task<IActionResult> DeleteComment([FromRoute] int movieId, int commentId, int userId);
    }
}
