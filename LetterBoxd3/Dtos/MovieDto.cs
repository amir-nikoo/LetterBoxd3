using LetterBoxdDomain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LetterBoxd3.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public required string ImageUrl { get; set; }

        [MaxLength(50)]
        public required string Title { get; set; }

        [MaxLength(200)]
        public required string Description { get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRating => Ratings?.Any() == true ? Math.Round(Ratings.Average(r => r.Score), 1) : 0;
        public virtual List<CommentGetDto>? Comments { get; set; }

        [JsonIgnore]
        public virtual List<Rating>? Ratings { get; set; }
    }
}
