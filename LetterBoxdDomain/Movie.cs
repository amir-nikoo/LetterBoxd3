using System.ComponentModel.DataAnnotations;

namespace LetterBoxdDomain
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public required string ImageUrl { get; set; }

        [MaxLength(50)]
        public required string Title { get; set; }

        [MaxLength(200)]
        public required string Description{ get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRating => Ratings?.Any() == true ? Math.Round(Ratings.Average(r => r.Score), 1): 0;
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<Rating>? Ratings { get; set; }
    }
}
