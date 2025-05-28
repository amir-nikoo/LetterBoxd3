using LetterBoxd3.Dtos;
using LetterBoxd3.Interfaces;
using LetterBoxdContext;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetterBoxd3.Services;
using Humanizer;

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
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Configurations", "banned_words.txt");
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
            if (string.IsNullOrWhiteSpace(comment))
                return false;

            var normalized = new string(comment
                .Where(c => char.IsLetter(c) || char.IsWhiteSpace(c))
                .ToArray())
                .ToLower();

            var compact = new string(normalized
                .Where(c => char.IsLetter(c))
                .ToArray());

            foreach (var bannedWord in _bannedWords)
            {
                if (normalized.Contains(bannedWord) || compact.Contains(bannedWord))
                    return true;
            }

            return false;
        }

        public async Task<ServiceResult<List<CommentGetDto>>> GetComments([FromRoute] int movieId)
        {
            var targetMovie = await _context.Movies
                .Include(m => m.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (targetMovie == null)
            {
                return ServiceResult<List<CommentGetDto>>.Fail(404, "Movie not found.");
            }

            var comments = targetMovie.Comments.Select(c => new CommentGetDto
            {
                Id = c.Id,
                Username = c.User?.UserName ?? "Deleted",
                Text = c.Text,
                TimeAgo = c.CreatedAt.Humanize()
            }).ToList();

            return ServiceResult<List<CommentGetDto>>.Successful(comments);
        }

        public async Task<ServiceResult<MovieDto>> PostComment(int movieId, int userId, CommentPostDto commentPostDto)
        {

            var commentExists = await _context.Comments.AnyAsync(c => c.MovieId == movieId && c.UserId == userId);
            if (commentExists)
                return ServiceResult<MovieDto>.Fail(403, "Can't post more than one comment. Try editing the comment you already posted.");

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
                return ServiceResult<MovieDto>.Fail(404, "Movie not found.");

            if (ContainsBannedWord(commentPostDto.Text))
                return ServiceResult<MovieDto>.Fail(403, "The comment contains inappropriate content.");

            var newComment = new Comment
            {
                MovieId = movieId,
                Text = commentPostDto.Text,
                UserId = userId
            };

            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return ServiceResult<MovieDto>.Successful(await _movieService.GetMovieWithDetails(movieId));
        }

        public async Task<ServiceResult<MovieDto>> EditComment(int movieId, int commentId, int userId, CommentPostDto commentPostDto)
        {
            var targetComment = await _context.Comments.FindAsync(commentId);
            if (targetComment == null)
                return ServiceResult<MovieDto>.Fail(404, "Comment not found.");

            if (targetComment.UserId != userId)
                return ServiceResult<MovieDto>.Fail(403, "This comment belongs to another user.");

            if (ContainsBannedWord(commentPostDto.Text))
                return ServiceResult<MovieDto>.Fail(403, "Edited comment contains inappropriate content.");

            targetComment.Text = commentPostDto.Text;
            await _context.SaveChangesAsync();

            return ServiceResult<MovieDto>.Successful(await _movieService.GetMovieWithDetails(movieId));
        }

        public async Task<ServiceResult<MovieDto>> DeleteComment(int movieId, int commentId, int userId)
        {
            var targetComment = await _context.Comments.FindAsync(commentId);
            if (targetComment == null)
                return ServiceResult<MovieDto>.Fail(404, "Comment not found.");

            if (targetComment.UserId != userId)
                return ServiceResult<MovieDto>.Fail(403, "This comment belongs to another user.");

            _context.Remove(targetComment);
            await _context.SaveChangesAsync();

            return ServiceResult<MovieDto>.Successful(await _movieService.GetMovieWithDetails(movieId));
        }

    }
}
