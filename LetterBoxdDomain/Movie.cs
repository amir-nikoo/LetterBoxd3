namespace LetterBoxdDomain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<Rating>? Ratings { get; set; }
    }
}
