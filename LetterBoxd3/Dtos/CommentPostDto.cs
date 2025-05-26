using System.ComponentModel.DataAnnotations;

namespace LetterBoxd3.Dtos
{
    public class CommentPostDto
    {
        [MaxLength(200, ErrorMessage = "Comment cannot be longer than 200 charracters")]
        public required string Text { get; set; }
    }
}
