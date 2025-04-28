using System.ComponentModel.DataAnnotations;

namespace LetterBoxd3.Dtos
{
    public class RatingDto
    {
        [Range(1, 5)]
        public required int Score { get; set; }
    }
}
