using System.ComponentModel.DataAnnotations;

namespace LetterBoxdDomain
{
    public class Rating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }
        public int? UserId { get; set; }
    }
}
