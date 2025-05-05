using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;
using LetterBoxdContext;
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
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _context.Movies
            .Select(m => new
            {
                m.Id,
                m.ImageUrl,
                m.Title
            })
            .ToListAsync();
            return new OkObjectResult(movies);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var targetMovie = await GetMovieWithDetails(id);

            if (targetMovie == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(targetMovie);
            }
        }

        public async Task<MovieDto> GetMovieWithDetails(int movieId)
        {
            var movie = await _context.Movies
        .Include(m => m.Comments)
            .ThenInclude(c => c.User)
        .Include(m => m.Ratings)
        .FirstOrDefaultAsync(m => m.Id == movieId);

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
                    Text = c.Text,
                    Username = c.User?.UserName ?? "Deleted"
                }).ToList()
            };
        }
    }
}
