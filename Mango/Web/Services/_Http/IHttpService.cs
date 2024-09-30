using Web.Models;

namespace Web.Services
{
    public interface IHttpService
    {
        Task<ResponseResult> SendAsync(RequestDTO request);
    }
}
