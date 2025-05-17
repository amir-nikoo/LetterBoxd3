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
        
        public async Task<ServiceResult<MovieDto>> PostRating(int movieId, int userId, RatingDto ratingDto)
        {
            var ratingExists = await _context.Ratings.AnyAsync(r => r.MovieId == movieId && r.UserId == userId);

            if (ratingExists)
                return ServiceResult<MovieDto>.Fail(403, "Can't rate a movie more than once. Try editing your rating.");

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
                return ServiceResult<MovieDto>.Fail(404, "Movie not found.");

            var newRating = new Rating
            {
                MovieId = movieId,
                Score = ratingDto.Score,
                UserId = userId
            };

            await _context.Ratings.AddAsync(newRating);
            await _context.SaveChangesAsync();

            return ServiceResult<MovieDto>.Successful(await _movieService.GetMovieWithDetails(movieId));
        }

        public async Task<ServiceResult<MovieDto>> EditRating(int movieId, int userId, int ratingId, RatingDto ratingDto)
        {
            var targetRating = await _context.Ratings.FindAsync(ratingId);
            if (targetRating == null)
                return ServiceResult<MovieDto>.Fail(404, "Rating not found.");

            if (targetRating.UserId != userId)
                return ServiceResult<MovieDto>.Fail(403, "This rating belongs to another user.");

            targetRating.Score = ratingDto.Score;
            await _context.SaveChangesAsync();

            return ServiceResult<MovieDto>.Successful(await _movieService.GetMovieWithDetails(movieId));
        }
    }
}
