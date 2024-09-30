using Web.Util;

namespace Web.Models
{
    public class RequestDTO
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; } = "";
        public object? Body { get; set; }
        //public string? AccessToken { get; set; }
    }
}
