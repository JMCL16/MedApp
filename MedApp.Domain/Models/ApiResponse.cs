namespace MedApp.Domain.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default(T)!;

        public static ApiResponse<T> SuccessResult(T data, string message = "Operacion exitosa") 
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResult(string message = "Operacion fallida")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default(T)!  
            };
        }
    }
}
