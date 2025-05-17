using LetterBoxd3.Dtos;
using LetterBoxdDomain;

namespace LetterBoxd3.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public T Data { get; set; }

        static public ServiceResult<T> Fail(int statusCode, string errorMessage)
        {
            return new ServiceResult<T>()
            {
                Success = false,
                StatusCode = statusCode,
                ErrorMessage = errorMessage
            };
        }

        static public ServiceResult<T> Successful(T data)
        {
            return new ServiceResult<T>()
            {
                Success = true,
                StatusCode = 200,
                Data = data
            };
        }
    }
}