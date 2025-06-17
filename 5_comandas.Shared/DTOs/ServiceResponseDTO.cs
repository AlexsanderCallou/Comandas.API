namespace Comandas.Shared.DTOs
{


    public class ServiceResponseDTO<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<ErroResult>? Errors { get; set; }

        public static ServiceResponseDTO<T> Ok(T data, string? message = null)
        {
            return new ServiceResponseDTO<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResponseDTO<T> Fail(string message, List<ErroResult>? errors = null)
        {
            return new ServiceResponseDTO<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }

        public static ServiceResponseDTO<T> Fail(List<ErroResult> errors)
        {
            return new ServiceResponseDTO<T>
            {
                Success = false,
                Errors = errors
            };
        }
    }

    public class ErroResult
    {
        public ErroResult(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        private int ErrorCode { get; set; }
        private string Message { get; set; } = default!;
        
    } 
}