using Humanizer;

namespace LetterBoxd3.Dtos
{
    public class CommentGetDto
    {
        public int Id { get; set; }
        public required string Text{ get; set; }
        public string? Username{ get; set; }
        public string TimeAgo { get; set; }
    }
}
