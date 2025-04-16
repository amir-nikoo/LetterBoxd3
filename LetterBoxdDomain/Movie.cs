using System.ComponentModel.DataAnnotations;

namespace LetterBoxdDomain
{
    public class Movie
    {
        public int Id { get; set; } //checking controller

        [MaxLength(256)]
        public required string ImageUrl { get; set; }

        [MaxLength(50)]
        public required string Title { get; set; }

        [MaxLength(200)]
        public required string Description{ get; set; }
        public int ReleaseYear { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<Rating>? Ratings { get; set; }
    }
}
