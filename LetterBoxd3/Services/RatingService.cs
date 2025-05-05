using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;
using LetterBoxdContext;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LetterBoxd3.Services
{
    public class RatingService : IRatingService
    {
        private readonly Context _context;
        private readonly IMovieService _movieService;
        public RatingService(Context context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }
        
        public async Task<IActionResult> PostRating(int movieId, int userId, RatingDto ratingDto)
        {
            var ratingExists = await _context.Ratings.AnyAsync(r => r.MovieId == movieId && r.UserId == userId);

            if (ratingExists)
                return new ForbidResult();

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
                return new NotFoundResult();

            var newRating = new Rating
            {
                MovieId = movieId,
                Score = ratingDto.Score,
                UserId = userId
            };

            await _context.Ratings.AddAsync(newRating);
            await _context.SaveChangesAsync();

            return new OkObjectResult(await _movieService.GetMovieWithDetails(movieId));
        }

        public async Task<IActionResult> EditRating(int movieId, int userId, int ratingId, RatingDto ratingDto)
        {
            var targetRating = await _context.Ratings.FindAsync(ratingId);
            if (targetRating == null)
                return new NotFoundResult();

            if (targetRating.UserId != userId)
                return new ForbidResult();

            targetRating.Score = ratingDto.Score;
            await _context.SaveChangesAsync();

            return new OkObjectResult(await _movieService.GetMovieWithDetails(movieId));
        }
    }
}
