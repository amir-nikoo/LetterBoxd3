using System.ComponentModel.DataAnnotations;

namespace LetterBoxd3.Dtos
{
    public class UserDto
    {
        [MaxLength(20)]
        public required string UserName { get; set; }

        [MinLength(8), MaxLength(20)]
        public required string Password { get; set; }
    }
}
