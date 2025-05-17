using LetterBoxd3.Dtos;
using LetterBoxd3.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface IMovieService
    {
        Task<ServiceResult<List<MoviesPreviewDto>>> GetMovies();
        Task<ServiceResult<MovieDto>> GetById(int id);
        Task<MovieDto> GetMovieWithDetails(int movieId);
    }
}
