using Humanizer;
using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;
using LetterBoxdContext;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LetterBoxd3.Services
{
    public class MovieService : IMovieService
    {
        private readonly Context _context;
        public MovieService(Context context)
        {
            _context = context;
        }
        public async Task<ServiceResult<List<MoviesPreviewDto>>> GetMovies()
        {
            var movies = await _context.Movies
            .Select(m => new MoviesPreviewDto
            {
                Id = m.Id,
                ImageUrl = m.ImageUrl,
                Title = m.Title
            })
            .ToListAsync();
            return ServiceResult<List<MoviesPreviewDto>>.Successful(movies);
        }

        public async Task<ServiceResult<MovieDto>> GetById(int id)
        {
            var targetMovie = await GetMovieWithDetails(id);

            if (targetMovie == null)
            {
                return ServiceResult<MovieDto>.Fail(404, "Movie not found.");
            }

            return ServiceResult<MovieDto>.Successful(targetMovie);
        }

        public async Task<MovieDto> GetMovieWithDetails(int movieId)
        {
            var movie = await _context.Movies
            .Include(m => m.Comments)
            .ThenInclude(c => c.User)
            .Include(m => m.Ratings)
            .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                return null;

            return new MovieDto
            {
                Id = movie.Id,
                ImageUrl = movie.ImageUrl,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Description = movie.Description,
                Ratings = movie.Ratings,
                Comments = movie.Comments.Select(c => new CommentGetDto
                {
                    Id = c.Id,
                    Username = c.User?.UserName ?? "Deleted",
                    Text = c.Text,
                    TimeAgo = c.CreatedAt.Humanize()
                }).ToList()
            };
        }
    }
}
