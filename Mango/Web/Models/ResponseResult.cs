namespace Web.Models
{
    public class ResponseResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static ResponseResult Success(object data)
        {
            return new ResponseResult
            {
                IsSuccess = true,
                Message = null,
                Data = data
            };
        }

        public static ResponseResult Error(string message)
        {
            return new ResponseResult
            {
                IsSuccess = false,
                Message = message,
                Data = null
            };
        }
    }
}
