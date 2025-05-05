using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;
using LetterBoxdContext;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LetterBoxd3.Services
{
    public class CommentService : ICommentService
    {
        private readonly Context _context;
        private readonly IMovieService _movieService;
        private readonly List<string> _bannedWords;

        public CommentService(Context context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "banned_words.txt");
            if (File.Exists(path))
            {
                _bannedWords = File.ReadAllLines(path)
                                   .Where(line => !string.IsNullOrWhiteSpace(line))
                                   .Select(line => line.Trim().ToLower())
                                   .ToList();
            }
            else
            {
                _bannedWords = new List<string>();
            }
        }

        public bool ContainsBannedWord(string comment)
        {
            var words = comment.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words.Any(w => _bannedWords.Contains(w));
        }

        public async Task<IActionResult> PostComment(int movieId, int userId, CommentPostDto commentPostDto)
        {
            var commentExists = await _context.Comments.AnyAsync(c => c.MovieId == movieId && c.UserId == userId);
            if (commentExists)
                return new ForbidResult();

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
                return new NotFoundResult();

            if (ContainsBannedWord(commentPostDto.Text))
                return new ForbidResult();

            var newComment = new Comment
            {
                MovieId = movieId,
                Text = commentPostDto.Text,
                UserId = userId
            };

            await _context.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return new OkObjectResult(await _movieService.GetMovieWithDetails(movieId));
        }

        public async Task<IActionResult> EditComment(int movieId, int commentId, int userId, CommentPostDto commentPostDto)
        {
            var targetComment = await _context.Comments.FindAsync(commentId);
            if (targetComment == null)
                return new NotFoundResult();

            if (targetComment.UserId != userId)
                return new ForbidResult();

            targetComment.Text = commentPostDto.Text;
            await _context.SaveChangesAsync();

            return new OkObjectResult(await _movieService.GetMovieWithDetails(movieId));
        }

        public async Task<IActionResult> DeleteComment(int movieId, int commentId, int userId)
        {
            var targetComment = await _context.Comments.FindAsync(commentId);
            if (targetComment == null)
                return new NotFoundResult();

            if (targetComment.UserId != userId)
                return new ForbidResult();

            _context.Remove(targetComment);
            await _context.SaveChangesAsync();

            return new OkObjectResult(await _movieService.GetMovieWithDetails(movieId));
        }
    }
}
