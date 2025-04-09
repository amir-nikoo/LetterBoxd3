using System.ComponentModel.DataAnnotations;

namespace LetterBoxdDomain
{
    public class Comment
    {
        public int Id { get; set; }
        public int MovieId{ get; set; }

        [MaxLength(50, ErrorMessage ="Comment cannot be longer than 50 charracters")]
        public required string Context { get; set; }
        public int UserId { get; set; }
    }
}
