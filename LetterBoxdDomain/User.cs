using System.ComponentModel.DataAnnotations;

namespace LetterBoxdDomain
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public required string UserName { get; set; }

        [MaxLength(128)]
        public required string PasswordHash { get; set; }
    }
}
