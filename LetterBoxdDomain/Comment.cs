using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LetterBoxdDomain
{
    public class Comment
    {
        public int Id { get; set; }
        public int MovieId{ get; set; }

        [MaxLength(200)]
        public required string Context { get; set; }
        public int? UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
