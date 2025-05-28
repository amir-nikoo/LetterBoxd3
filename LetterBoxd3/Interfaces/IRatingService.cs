using LetterBoxd3.Dtos;
using LetterBoxd3.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface IRatingService
    {
        Task<ServiceResult<int>> GetRating(int movieId, int userId);
        Task<ServiceResult<MovieDto>> PostRating(int movieId,int userId, RatingDto ratingDto);
        Task<ServiceResult<MovieDto>> EditRating(int movieId, int userId, RatingDto ratingDto);
    }
}
