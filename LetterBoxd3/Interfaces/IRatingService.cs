using LetterBoxd3.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface IRatingService
    {
        Task<IActionResult> PostRating(int movieId,int userId, RatingDto ratingDto);
        Task<IActionResult> EditRating(int movieId, int userId, int ratingId, RatingDto ratingDto);
    }
}
