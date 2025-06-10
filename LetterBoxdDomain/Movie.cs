using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LetterBoxdDomain
{
    public class Movie
    {
        public int Id { get; set; }

        [Column(TypeName = "character varying(256)")]
        [MaxLength(256)]
        public required string ImageUrl { get; set; }

        [Column(TypeName = "character varying(50)")]
        [MaxLength(50)]
        public required string Title { get; set; }

        [Column(TypeName = "character varying(300)")]
        [MaxLength(300)]
        public required string Description{ get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRating => Ratings?.Any() == true ? Math.Round(Ratings.Average(r => r.Score), 1): 0;
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<Rating>? Ratings { get; set; }
    }
}
