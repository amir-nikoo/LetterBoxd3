using System.ComponentModel.DataAnnotations;

namespace LetterBoxd3.Dtos
{
    public class MoviesPreviewDto
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public required string ImageUrl { get; set; }

        [MaxLength(50)]
        public required string Title { get; set; }
    }
}
