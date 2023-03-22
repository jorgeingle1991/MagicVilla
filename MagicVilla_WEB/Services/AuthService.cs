using MagicVilla_Utility;
using MagicVilla_WEB.Models;
using MagicVilla_WEB.Models.Authentication.Dto;
using MagicVilla_WEB.Services.IServices;
using System.Runtime.CompilerServices;

namespace MagicVilla_WEB.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;
        public AuthService(IHttpClientFactory clientFactory,IConfiguration configuration):base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> LogingAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/v1/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
           return SendAsync<T>(new APIRequest(){
                ApiType = SD.ApiType.POST,
                Data=obj,
                Url = villaUrl + "/api/v1/UsersAuth/login"
            });
        }
    }
}
