namespace Comandas.Shared.DTOs
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ServiceResponse<T> Ok(T data, string? message = null)
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResponse<T> Fail(string message, List<string>? errors = null)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }

        public static ServiceResponse<T> Fail(List<string> errors)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Errors = errors
            };
        }
    }
}