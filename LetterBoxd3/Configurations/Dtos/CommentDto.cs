using System.ComponentModel.DataAnnotations;

namespace LetterBoxd3.Configurations.Dtos
{
    public class CommentDto
    {
        [MaxLength(100, ErrorMessage = "Comment cannot be longer than 200 charracters")]
        public required string Context { get; set; }
    }
}
