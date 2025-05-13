namespace LetterBoxd3.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public enum Result
        {
            notFound,
            alreadyHas,
            badWord,
            badReq
        }

        public string ErrorMessage { get; set; }
    }
}
