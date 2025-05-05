using LetterBoxd3.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface IMovieService
    {
        Task<IActionResult> GetMovies();
        Task<IActionResult> GetById(int id);
        Task<MovieDto> GetMovieWithDetails(int movieId);
    }
}
