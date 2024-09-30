
using Newtonsoft.Json;

using System.Net;
using System.Net.Http.Headers;
using System.Text;

using Web.Models;
using Web.Util;

namespace Web.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenService _tokenService;

        public HttpService(IHttpClientFactory clientFactory,
            ITokenService tokenService)
        {
            _clientFactory = clientFactory;
            _tokenService = tokenService;
        }



        public async Task<ResponseResult> SendAsync(RequestDTO request)
        {
            try
            {
                HttpClient client = _clientFactory.CreateClient("Mango");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                // adding token 

                var token = _tokenService.GetToken();
                if (!string.IsNullOrEmpty(token))
                {
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                message.RequestUri = new Uri(request.Url);

                if (request.ApiType == ApiType.POST || request.ApiType == ApiType.PUT)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");
                }

                message.Method = request.ApiType switch
                {
                    ApiType.GET => HttpMethod.Get,
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

              

                HttpResponseMessage response = await client.SendAsync(message);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return ResponseResult.Error("Not Found");

                    case HttpStatusCode.Unauthorized:
                        return ResponseResult.Error("Unauthorized");

                    case HttpStatusCode.Forbidden:
                        return ResponseResult.Error("Unauthorized");

                    case HttpStatusCode.BadRequest:
                        return ResponseResult.Error("Bad Request");

                    case HttpStatusCode.InternalServerError:
                        return ResponseResult.Error("Internal Server Error");


                    default:
                        var apiContent = await response.Content.ReadAsStringAsync();
                        var responseDto = JsonConvert.DeserializeObject<ResponseResult>(apiContent);
                        return responseDto ?? ResponseResult.Error("NoContent");
                }
            }
            catch (Exception ex)
            {
                return ResponseResult.Error(ex.Message);
            }
        }



    }
}
