using LetterBoxd3.Dtos;
using LetterBoxd3.Services;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxd3.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<bool>> Register(UserDto userDto);
        Task<ServiceResult<LoginResultDto>> Login(UserDto userDto);
    }
}
