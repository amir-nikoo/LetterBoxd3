using LetterBoxd3.Dtos;
using LetterBoxd3.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface ICommentService
    {
        Task<ServiceResult<MovieDto>> PostComment([FromRoute] int movieId, int userId, CommentPostDto commentPostDto);
        Task<ServiceResult<MovieDto>> EditComment([FromRoute] int movieId, int commentId, int userId, CommentPostDto commentPostDto);
        Task<ServiceResult<MovieDto>> DeleteComment([FromRoute] int movieId, int commentId, int userId);
    }
}
