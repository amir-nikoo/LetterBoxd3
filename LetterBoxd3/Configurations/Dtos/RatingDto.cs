using System.ComponentModel.DataAnnotations;

namespace LetterBoxd3.Configurations.Dtos
{
    public class RatingDto
    {
        [Range(1, 5)]
        public required int Score { get; set; }
    }
}
