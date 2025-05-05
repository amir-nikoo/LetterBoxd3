using LetterBoxd3.Dtos;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> Register(UserDto userDto);
        Task<IActionResult> Login(UserDto userDto);
    }
}
