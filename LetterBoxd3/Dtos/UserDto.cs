using System.ComponentModel.DataAnnotations;

namespace LetterBoxd3.Dtos
{
    public class UserDto
    {
        [MaxLength(20, ErrorMessage = "Username can not have more than 20 characters")]
        public required string UserName { get; set; }

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long."), MaxLength(20, ErrorMessage = "Password can not have more than 20 characters")]
        public required string Password { get; set; }
    }
}
